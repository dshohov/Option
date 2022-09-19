using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OptionWebApplication.Models;

namespace OptionWebApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ) : base(options)
        {
            
        }
        public DbSet<Assembly> Assemblies { get; set; }
        public DbSet<Guarentee> Guarentes { get; set; }
        public DbSet<AssemblyFiles> Files { get; set; }
        public DbSet<Sertification> Sertifications { get; set; }
    }
}
