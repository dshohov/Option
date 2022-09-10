using OptionWebApplication.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace OptionWebApplication.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Company { get; set; }
        public TypeDevice TypeDevice { get; set; }
    }
}
