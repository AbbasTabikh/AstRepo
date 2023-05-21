using System.ComponentModel.DataAnnotations;

namespace Demo.Api.InputModels
{
    public class ProjectInputModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;

        public byte[]? Image { get; set; }
    }
}
