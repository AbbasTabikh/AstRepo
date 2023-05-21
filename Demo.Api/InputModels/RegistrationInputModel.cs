using System.ComponentModel.DataAnnotations;

namespace Demo.Api.InputModels
{
    public class RegistrationInputModel
    {
        [Required]
        [MaxLength(35)]
        public string Username { get; set; } = string.Empty;

        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\+?[1-9][0-9]{7,14}$")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
