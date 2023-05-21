using Demo.Api.Dtos;
using Demo.Data.Models;
using System.Diagnostics.CodeAnalysis;

namespace Demo.Api.Mapping
{
    public static class ApplicationUserMapping
    {
        public static ApplicationUserDto ToDto( this ApplicationUser applicationUser )
        {
            return new ApplicationUserDto
            {
                EmailAddress = applicationUser.Email!,
                PhoneNumber = applicationUser.PhoneNumber!,
                Username = applicationUser.UserName!
            };
        }
    }
}
