using System.ComponentModel.DataAnnotations;

namespace OptionWebApplication.Models
{
    public class Guarentee
    {
        [Key]
        public int Id { get; set; }
        public int SerialNumber { get; set; }
        public string TypeDevice { get; set; }
        public DateTime DateIn { get; set; }
        public DateTime DateOut { get; set; }
        public string? Details { get; set; }//reiconete
        public string? FaultDetection { get; set; }
        public string? Conclusion { get; set; }
        public string DiagnosticPeople { get; set; }
        public string? ComplectedWork { get; set; }
        public string RepairPeople { get; set; }
    }
}
