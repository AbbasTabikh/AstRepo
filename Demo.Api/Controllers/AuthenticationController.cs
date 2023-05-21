using Demo.Api.InputModels;
using Demo.Api.Services;
using Demo.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Demo.Api.Controllers
{
    //test
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public AuthenticationController(IConfiguration configuration, IUserService userService )
        {
            _configuration = configuration;
            _userService = userService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationInputModel registrationUser)
        {
            var result = await _userService.GetByEmailAsync(registrationUser.EmailAddress);

            //email already exists
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }

            (IdentityResult, ApplicationUser) creationResult = await _userService.RegisterAsync(registrationUser);

            if (creationResult.Item1.Succeeded)
            {
                //returns Ok(object)
                return GenerateToken(creationResult.Item2);
            }

            return BadRequest(creationResult.Item1);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginInputModel loginUser, CancellationToken token)
        {

            (IdentityResult, ApplicationUser) loginResult = await _userService.LoginAsync(loginUser, token);

            if (loginResult.Item1.Succeeded)
            {
                //returns Ok(object)
                return GenerateToken(loginResult.Item2);
            }

            return Unauthorized(loginResult.Item1);
        }


        private IActionResult GenerateToken(ApplicationUser user)
        {
            List<Claim> userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email , user?.Email!),
                new Claim(ClaimTypes.Name, user?.UserName!) ,
                new Claim(ClaimTypes.MobilePhone, user?.PhoneNumber!),
            };


            foreach (var userrole in user!.UserRoles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, userrole?.Role?.Name!));
            }


            string jwtKey = _configuration.GetSection("JwtKey:SecretKey").Value!;
            var secretKeyBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var cred = new SigningCredentials(secretKeyBytes, SecurityAlgorithms.HmacSha512Signature);

            var jwtSecurityToken = new JwtSecurityToken(claims: userClaims, expires: DateTime.Now.AddDays(1), signingCredentials: cred);



            return Ok(
                new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Expiration = jwtSecurityToken.ValidTo,
                    Roles = user.UserRoles.Select(r => r.Role.Name).ToArray(),
                    Username = user.UserName,
                    EmailAddress = user.Email,
                    Phone = user.PhoneNumber
                });
        }
    }
}
