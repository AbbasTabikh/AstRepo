using System.ComponentModel.DataAnnotations;

namespace Demo.Api.InputModels
{
    public class LoginInputModel
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
