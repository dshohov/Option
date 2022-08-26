using System.ComponentModel.DataAnnotations;

namespace OptionWebApplication.Models
{
    public class Assembly
    {
        [Key]
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string ChangeComponents { get; set; }
        public string OtherWork { get; set; }

    }
}
