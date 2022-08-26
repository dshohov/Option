using System.ComponentModel.DataAnnotations;

namespace OptionWebApplication.Models
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        public int SerialNumber { get; set; }
        public string TypeDevice { get; set; }
    }
}
