using Demo.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Demo.Api.InputModels
{
    public class TaskInputModel
    {
        [Required]
        [MaxLength(30)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;

        public Status Status { get; set; } = Status.Waiting;

        public IEnumerable<Guid> Projects { get; set; } = new List<Guid>();
    }
}
