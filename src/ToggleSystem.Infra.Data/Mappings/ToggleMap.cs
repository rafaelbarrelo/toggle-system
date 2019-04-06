using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToggleSystem.Domain.Entities;

namespace ToggleSystem.Infra.Data.Mappings
{
    public class ToggleMap : IEntityTypeConfiguration<Toggle>
    {
        public void Configure(EntityTypeBuilder<Toggle> builder)
        {
            builder.HasIndex(c => c.Name);
            builder.HasIndex(c => c.Version);
            builder.HasIndex(c => c.IsDeleted);
        }
    }
}
