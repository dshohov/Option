using OptionWebApplication.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace OptionWebApplication.ViewModels
{
    public class EditGuarenteeViewModel : EditDeviceViewModel
    {
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public string? Details { get; set; }//reiconete
        public string? FaultDetection { get; set; }
        public string? Conclusion { get; set; }
        public People DiagnosticPeople { get; set; }
        public string? ComplectedWork { get; set; }
        public People RepairPeople { get; set; }
    }
}
