using Demo.Api.InputModels;
using Demo.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Demo.Api.Services
{
    public interface IUserService
    {
        Task<(IdentityResult, ApplicationUser)> RegisterAsync(RegistrationInputModel registrationUser);
        Task<IdentityResult> GetByEmailAsync(string emailAddress);
        Task<(IdentityResult, ApplicationUser)> LoginAsync(LoginInputModel loginUser, CancellationToken cancellationToken);
    }
}
