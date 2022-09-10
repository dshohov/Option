using OptionWebApplication.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace OptionWebApplication.Models
{
    public class Assembly : Device
    {
 
        public bool CheckEngenire { get; set; } //проверил инженер ОТК нею
        public DateTime DateCreate { get; set; } // дата создания нею
        public string? Component { get; set; } // компонент который надо заменить нею
        public string? ChangeComponents { get; set; } //компонент на который меняем
        public string? OtherWork { get; set; } // дргуие работы 
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
        public string? Sertification { get; set; }
        public string? Signature { get; set; }
    }
}
