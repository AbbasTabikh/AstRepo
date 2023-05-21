namespace Demo.Api.Dtos
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[]? Image { get; set; }
        public string Description { get; set; } = string.Empty;

        public ICollection<string> Tasks { get; set; } = new List<string>();
    }
}
