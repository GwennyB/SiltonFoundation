using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace SiltonFoundation.Models.ViewModels
{
    public class SFAppViewModel
    {
        public SFApp Appl { get; set; }
        public List<Question> Questions { get; set; }
        public List<AppResponse> AppResponses { get; set; }
        public List<Evaluation> Evaluations { get; set; }
        public List<string> Categories { get; set; }
    }
}
