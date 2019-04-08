using System;

namespace ToggleSystem.Domain.Entities
{
    public class ToggleUser : BaseEntity
    {
        public int ToggleId { get; set; }
        public Guid UserId { get; set; }
        public ToggleValue ToggleValue { get; set; }
    }
}
