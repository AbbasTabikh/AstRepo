using Demo.Api.InputModels;
using Demo.Data.Models;

namespace Demo.Api.Mapping
{
    public static class RegistrationResourcesMapping
    {
        public static ApplicationUser ToApplicationUser(this RegistrationInputModel registrationUser)
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = registrationUser.Username,
                Email = registrationUser.EmailAddress,
                PhoneNumber = registrationUser.PhoneNumber
            };
            return user;
        }
    }
}
