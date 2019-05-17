using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SiltonFoundation.Models
{
    public class Evaluation
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Email { get; set; }
        public string Name { get; set; }
        [Required]
        public DateTime LastChange { get; set; }
        public DateTime Closed { get; set; }

        [Required]
        [Range(1,100)]
        public int Score { get; set; }

        // Foreign keys
        [Required]
        public int SFAppID { get; set; }
        [Required]
        public int QuestionID { get; set; }

        // Navigation Properties
        public SFApp SFApp { get; set; }
        public Question Question { get; set; }
    }
}
