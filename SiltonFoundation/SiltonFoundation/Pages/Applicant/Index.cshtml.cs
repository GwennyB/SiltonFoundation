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

namespace SiltonFoundation.Pages.Applicant
{
    [Authorize(Roles = "Applicant, General")]
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
        public bool ViewApp { get; set; } = false; // used to temporarily hold email of user to manage
        public bool PromptSubmit { get; set; } = false; // used to temporarily hold email of user to manage
        public UpdateProfileViewModel UpdateProfileVM { get; set; }

        public SFAppViewModel SFAppVM { get; set; }


        public async Task OnGet()
        {
            SFAppVM = await _app.BuildSFAppVM(User.Identity.Name);
        }



        // To manage user profiles
        public async Task OnPostGetProfile()
        {
            if (ManagedUserEmail != null)
            {
                UpdateProfileVM = await _appUser.BuildUPVM(ManagedUserEmail);
            }
        }

        public async Task OnPostUpdateProfile(UpdateProfileViewModel bag)
        {
            UpdateProfileVM = ModelState.IsValid ? await _appUser.UpdateUserProfile(bag) : bag;
        }



        public async Task OnGetStartApplication()
        {
            // get logged in user's info
            AppUser user = await _user.FindByEmailAsync(User.Identity.Name);

            // if user is not 'Applicant', add Applicant role
            if (!await _user.IsInRoleAsync(user, AppRoles.Applicant))
            {
                await _user.AddToRoleAsync(user, AppRoles.Applicant);
            }

            // create new application
            SFApp newApp = await _app.CreateSFAppAsync(user.Email);
            SFAppVM = await _app.BuildSFAppVM(User.Identity.Name);

        }

        // To view an application for a selected applicant
        public async Task OnGetViewAppl()
        {
            SFAppVM = await _app.BuildSFAppVM(User.Identity.Name);
            ViewApp = true;
        }

        public async Task OnGetReadySubmit()
        {
            SFAppVM = await _app.BuildSFAppVM(User.Identity.Name);
            PromptSubmit = true;
        }

        // Submit App
        public async Task OnPostSubmitAppl()
        {
            SFAppVM = await _app.BuildSFAppVM(User.Identity.Name);

            SFApp appl = await _app.GetSFAppAsync(User.Identity.Name);
            appl.AppStatus = AppStatus.Submitted;
            appl = await _app.UpdateSFAppAsync(appl);
            if (appl.AppStatus == AppStatus.Submitted)
            {
                // build email
                Email message = new Email()
                {
                    Recipient = User.Identity.Name,
                    ConfigSet = "",
                    Subject = "Your Silton Foundation grant application has been received!",
                    BodyHtml = @"<html>
                                <head></head>
                                <body>
                                  <h1>Thank you for applying!</h1>
                                </body>
                                </html>",

                };
                bool emailStatus = await message.Send();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Submit failed. Please try again.");
            }
            //await _eval.CreateEvaluationsForAppAsync(appl.ID);
        }

        // Save Applicant app response updates
        public async Task OnPostUpdateAppResponses(SFAppViewModel sFApp)
        {
            foreach (AppResponse item in sFApp.AppResponses)
            {
                await _response.UpdateAppResponseAsync(item);
            }
            SFAppVM = await _app.BuildSFAppVM(User.Identity.Name);
            ViewApp = true;
        }
    }
}