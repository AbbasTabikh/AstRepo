using System.ComponentModel.DataAnnotations;

namespace Demo.Api.InputModels
{
    public class ContactRequestInputModel
    {
        [Required , MaxLength(56)]
        public string UserFullName { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
        public string? IPAddress { get; set; }

        [Required , MaxLength(500)]
        public string Content { get; set; } = string.Empty;
    }
}
