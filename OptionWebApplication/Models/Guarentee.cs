using OptionWebApplication.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace OptionWebApplication.Models
{
    public class Guarentee : Device
    {
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public string? Details { get; set; }//рекламация
        public string? FaultDetection { get; set; }
        public string? Conclusion { get; set; }
        public People DiagnosticPeople { get; set; }
        public string? ComplectedWork { get; set; }
        public People RepairPeople { get; set; }
    }
}
