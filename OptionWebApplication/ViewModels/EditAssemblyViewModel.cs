﻿using System.ComponentModel.DataAnnotations;

namespace OptionWebApplication.ViewModels
{
    public class EditAssemblyViewModel
    {
        [Key]
        public int Id { get; set; }
        public int SerialNumber { get; set; }
        public string TypeDevice { get; set; }
        public string? ChangeComponents { get; set; }
        public string? OtherWork { get; set; }
        public string? Steps { get; set; }
        public string People { get; set; }
    }
}