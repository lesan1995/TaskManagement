using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace TaskManagement.Infrastructure.Identity
{
    public class AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
