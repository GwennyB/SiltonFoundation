using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SiltonFoundation.Models
{
    public class AppResponse
    {
        [Required]
        public int ID { get; set; }



        // Foreign Keys
        [Required]
        public int SFAppID { get; set; }
        [Required]
        public int QuestionID { get; set; }

        public string Answer { get; set; }

        public ICollection<Evaluation> Evaluations { get; set; }

        // Navigation Properties
        public SFApp SFApp { get; set; }
        public Question Question { get; set; }
    }
}
