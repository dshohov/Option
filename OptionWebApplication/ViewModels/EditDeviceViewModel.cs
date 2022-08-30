using OptionWebApplication.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace OptionWebApplication.ViewModels
{
    public class EditDeviceViewModel
    {

        
        
        public int SerialNumber { get; set; }
        public string Company { get; set; }
        public TypeDevice TypeDevice { get; set; }
        public string? SerialNumberParty { get; set; }
    }
}
