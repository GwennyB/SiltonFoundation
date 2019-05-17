using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SiltonFoundation.Models;
using SiltonFoundation.Models.Interfaces;
using SiltonFoundation.Models.ViewModels;

namespace SiltonFoundation.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _user;
        private readonly ISFAppManager _app;
        private readonly IAppResponseManager _response;
        private readonly IQuestionManager _question;
        private readonly IEvaluationManager _eval;
        private readonly IAppUserManager _appUser;

        public IndexModel(UserManager<AppUser> userManager, ISFAppManager appManager, IAppResponseManager respManager, IQuestionManager questionManager, IEvaluationManager eval, IAppUserManager appUser)
        {
            _user = userManager;
            _app = appManager;
            _response = respManager;
            _question = questionManager;
            _eval = eval;
            _appUser = appUser;
        }

        // properties
        [BindProperty]
        public string ManagedUserEmail { get; set; } // used to temporarily hold email of user to manage

        [BindProperty]
        public string ApplicantEmail { get; set; } // used to temporarily hold email of applicant to view
        //public SFAppViewModel SFAppVM { get; set; } // holds ViewModel to load Application Partial

        public string AdminEmail { get; set; } // used to grab application for viewing

        public List<SFApp> CurrentApps { get; set; }
        public List<Question> AllQuestions { get; set; }
        [BindProperty]
        public int ManagedQuestionID { get; set; }
        public Question ManagedQuestion { get; set; }
        public IList<AppUser> Admins { get; set; }
        public IList<AppUser> Evaluators { get; set; }
        public IList<AppUser> Applicants { get; set; }
        public IList<AppUser> GeneralUsers { get; set; }
        public SFAppViewModel SFAppVM { get; set; }
        public UpdateProfileViewModel UpdateProfileVM { get; set; }


        // Initial page load
        public async Task OnGet()
        {
            await PopulateModel();
        }

        // To manage user profiles
        public async Task OnPostGetProfile()
        {
            await PopulateModel();
            if (ManagedUserEmail != null)
            {
                UpdateProfileVM = await _appUser.BuildUPVM(ManagedUserEmail);
            }
        }

        public async Task OnPostUpdateProfile(UpdateProfileViewModel bag)
        {
            await PopulateModel();
            UpdateProfileVM = ModelState.IsValid ? await _appUser.UpdateUserProfile(bag) : bag;
        }


        // To view an application for a selected applicant
        public async Task OnPostGetAppl()
        {
            await PopulateModel();
            if (ApplicantEmail != "null")
            {
                SFAppVM = await _app.BuildSFAppVM(ApplicantEmail);
            }
            else
            {
                ApplicantEmail = null;
            }
        }

        // TODO: Turn on this feature during Evaluations feature deployment
        //// Save Evaluator app evaluation updates
        //public async Task OnPostUpdateAppEval(SFAppViewModel sFApp)
        //{
        //    await PopulateModel();
        //    foreach (Evaluation item in sFApp.Evaluations)
        //    {
        //        await _eval.UpdateEvaluationAsync(item);
        //    }
        //    SFAppVM = await _app.BuildSFAppVM(sFApp.Appl.Email);
        //}

        // To manage Questions in the DB
        public async Task OnGetManageQuestions()
        {
            await PopulateModel();
            AllQuestions = await _question.GetAllQuestionsAsync();
        }

        public async Task OnPostUpdateQuestions()
        {
            await PopulateModel();
            AllQuestions = await _question.GetAllQuestionsAsync();
        }

        public async Task OnPostSelectQuestion()
        {
            await PopulateModel();
            ManagedQuestion = await _question.GetQuestionAsync(ManagedQuestionID);
            AllQuestions = await _question.GetAllQuestionsAsync();
        }

        // These routes are for managing user roles
        public async Task OnPostMakeEval()
        {
            await _user.AddToRoleAsync(await _user.FindByEmailAsync(ManagedUserEmail), AppRoles.Evaluator);
            await PopulateModel();
        }

        public async Task OnPostRemoveEval()
        {
            await _user.RemoveFromRoleAsync(await _user.FindByEmailAsync(ManagedUserEmail), AppRoles.Evaluator);
            await PopulateModel();
        }

        public async Task OnPostMakeAdmin()
        {
            await _user.AddToRoleAsync(await _user.FindByEmailAsync(ManagedUserEmail), AppRoles.Admin);
            await PopulateModel();
        }

        public async Task OnPostRemoveAdmin()
        {
            await _user.RemoveFromRoleAsync(await _user.FindByEmailAsync(ManagedUserEmail), AppRoles.Admin);
            await PopulateModel();
        }



        // helper methods
        private async Task PopulateModel()
        {
            AdminEmail = User.Identity.Name;
            GeneralUsers = await _user.GetUsersInRoleAsync("General");
            Admins = (await _user.GetUsersInRoleAsync("Admin")).Where(g => g.Email != "admin@thesiltonfoundation.org").ToList();
            Evaluators = await _user.GetUsersInRoleAsync("Evaluator");
            Applicants = await _user.GetUsersInRoleAsync("Applicant");
            CurrentApps = await _app.GetAllAppsAsync(1900, DateTime.Now.Year);
            foreach (SFApp appl in CurrentApps)
            {
                appl.Evaluations = await _eval.GetAllEvaluationsAsync(appl.ID);
            }
        }
    }
}