using Demo.Data.Enums;

namespace Demo.Api.Dtos
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.Waiting;
        public IEnumerable<string> ProjectsNames { get; set; } = default!;
    }
}
