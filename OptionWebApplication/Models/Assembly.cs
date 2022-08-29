using OptionWebApplication.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace OptionWebApplication.Models
{
    public class Assembly
    {
        [Key]
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Company { get; set; }
        public string TypeDevice { get; set; }
        public string? ChangeComponents { get; set; }
        public string? OtherWork { get; set; }
        public string Step1 { get; set; }
        public string? Step2 { get; set; }
        public string? Step3 { get; set; }
        public string? Step4 { get; set; }
        public string? Step5 { get; set; }
        public People People1 { get; set; }
        public People? People2 { get; set; }
        public People? People3 { get; set; }
        public People? People4 { get; set; }
        public People? People5 { get; set; }

    }
}
