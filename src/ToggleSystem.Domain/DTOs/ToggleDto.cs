using ToggleSystem.Domain.Entities;

namespace ToggleSystem.Domain.DTOs
{
    public class ToggleDto
    {
        public int Id { get; set; }
        public int Version { get; set; }
        public string Name { get; set; }
        public ToggleValue DefaultValue { get; set; }
        public ToggleValue? ToggleValue { get; set; }
    }
}

