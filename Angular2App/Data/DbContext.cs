using Angular2App.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Angular2App.Data
{
    public class DbContext : IdentityDbContext<ApplicationUser>
    {
       
        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {
        }
        public DbContext db { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
