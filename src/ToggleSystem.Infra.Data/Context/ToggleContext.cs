using Microsoft.EntityFrameworkCore;

namespace ToggleSystem.Infra.Data.Context
{
    public class ToggleContext : DbContext
    {
        public ToggleContext(DbContextOptions<ToggleContext> options) : base(options) { }
    }
}
