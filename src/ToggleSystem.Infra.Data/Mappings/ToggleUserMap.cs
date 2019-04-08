using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToggleSystem.Domain.Entities;

namespace ToggleSystem.Infra.Data.Mappings
{
    public class ToggleUserMap : IEntityTypeConfiguration<ToggleUser>
    {
        public void Configure(EntityTypeBuilder<ToggleUser> builder)
        {
            builder.HasIndex(c => c.ToggleValue);
            builder.HasIndex(c => c.UserId);
        }
    }
}
