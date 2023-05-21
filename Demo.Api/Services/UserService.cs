using Demo.Api.InputModels;
using Demo.Api.Mapping;
using Demo.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Demo.Api.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<ApplicationUser> userManager,
                           RoleManager<ApplicationRole> roleManager,
                           SignInManager<ApplicationUser> signInManager,
                           IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<(IdentityResult, ApplicationUser)> RegisterAsync(RegistrationInputModel registrationUser)
        {
            var appUser = registrationUser.ToApplicationUser();

            //create the user
            var result = await _userManager.CreateAsync(appUser, registrationUser.Password);

            if (result.Succeeded)
            {
                //get the new user role
                var userRole = await _roleManager.FindByNameAsync("User");

                //add the role to the user
                await _userManager.AddToRoleAsync(appUser, userRole!.Name!);
            }

            return (result, appUser);
        }
        public async Task<IdentityResult> GetByEmailAsync(string emailAddress)
        {
            var user = await _userManager.FindByEmailAsync(emailAddress);

            //email already registered
            if (user != null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Email already registered" });
            }

            return IdentityResult.Success;
        }
        public async Task<(IdentityResult, ApplicationUser)> LoginAsync(LoginInputModel loginUser, CancellationToken cancellationToken)
        {
            var appUser = await _userManager.Users
                                      .Include(x => x.UserRoles)
                                      .ThenInclude(x => x.Role)
                                      .SingleOrDefaultAsync(x => x.Email == loginUser.EmailAddress, cancellationToken);

            if (appUser == null)
            {
                return (IdentityResult.Failed(new IdentityError { Description = "Email doesn't exist" }), null)!;
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(appUser, loginUser.Password, false);

            //login success
            if (signInResult.Succeeded)
            {
                return (IdentityResult.Success, appUser);
            }

            return (IdentityResult.Failed(new IdentityError { Description = "Entered password is not correct" }), null)!;
        }
    }
}
