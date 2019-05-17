using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SiltonFoundation.Models
{
    public class Question
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public bool Active { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Prompt { get; set; }

        [Required]
        public ResponseType ResponseType { get; set; }

        public string ResponseOptions { get; set; }

        public bool Score { get; set; }
    }

    public enum ResponseType
    {
        [Display(Name = "Free-form text")]
        [Description("Free-form text")]
        Long_Text,
        [Display(Name = "Short text")]
        [Description("Short text")]
        Short_Text,
        [Display(Name = "Select from list")]
        [Description("Select from list")]
        Select
    }
}
