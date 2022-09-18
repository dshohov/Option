using Microsoft.AspNetCore.Identity;

namespace OptionWebApplication.Models
{
    public class AppUser : IdentityUser
    {

        public int? Pace { get; set; }
        public int? Mileage { get; set; }
        public ICollection<Assembly> Assemblies { get; set; }
        public ICollection<Guarentee> Guarentees { get; set; }
    }
}
