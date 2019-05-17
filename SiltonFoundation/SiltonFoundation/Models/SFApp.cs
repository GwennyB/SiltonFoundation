using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SiltonFoundation.Models
{
    public class SFApp
    {

        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public int AppYear { get; set; }
        [Required]
        public Grant ApplyingFor { get; set; }

        public bool Awarded { get; set; }
        public int Amount { get; set; }
        public string AdminEmail { get; set; }

        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime LastChange { get; set; }
        [Required]
        public AppStatus AppStatus { get; set; }
        public DateTime? Closed { get; set; }

        [NotMapped]
        public List<AppResponse> AppResponses { get; set; }

        [NotMapped]
        public List<Question> Questions { get; set; }

        [NotMapped]
        public List<string> Categories { get; set; }

        [NotMapped]
        public List<Evaluation> Evaluations { get; set; }
    }

    public enum AppStatus
    {
        Draft,
        Submitted,
        [Description("Under Evaluation")]
        UnderEval,
        Complete
    }

    public enum Grant
    {
        [Description("The West Coast Grant")]
        WestCoastGrant,
        [Description("The National Grant")]
        NationalGrant
    }
}
