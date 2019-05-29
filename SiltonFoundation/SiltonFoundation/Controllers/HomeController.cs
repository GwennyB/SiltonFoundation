using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiltonFoundation.Models;
using SiltonFoundation.Models.Interfaces;
using SiltonFoundation.Models.ViewModels;

namespace SiltonFoundation.Controllers
{
    public class HomeController : Controller
    {
        private IAppUserManager _user;
        private SignInManager<AppUser> _signInManager;

        /// <summary>
        /// constructs AppUserController object to manager user account creation and sign-in
        /// </summary>
        /// <param name="userManager"> user manager service context </param>
        /// <param name="signInManager"> signIn manager service context </param>
        public HomeController(IAppUserManager userManager, SignInManager<AppUser> signInManager)
        {
            _user = userManager;
            _signInManager = signInManager;
        }


        /// <summary>
        /// GET: Home/Index - 
        /// loads Home page
        /// displays login form if no logged in user
        /// routes to Dashboard if user is logged in
        /// </summary>
        /// <returns> Home page with login form or redirect to role-specific Dashboard </returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("error/{code}")]
        [Route("")]
        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToPage("/Admin/Index");
                }
                if (User.IsInRole("Evaluator"))
                {
                    return RedirectToPage("/Evaluator/Index");
                }
                if (User.IsInRole("Applicant") || User.IsInRole("General"))
                {
                    return RedirectToPage("/Applicant/Index");
                }
                return View();
            }
            return View();
        }

        /// <summary>
        /// GET: Home/Register - 
        /// loads Home page with Registration form
        /// </summary>
        /// <returns> Home page with registration form </returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// POST: Home/Register - 
        /// submits registration data
        /// logs in new user and redirects to Applicant Dashboard if successful
        /// returns Home page with Registration form (with error messages and user data loaded) until successful
        /// </summary>
        /// <param name="bag"> data submitted by user on Registration form </param>
        /// <returns> auto login and redirect to user's dashboard if successful account creation, reloads Home with Registration form and error messages if not </returns>
        [HttpPost]
        [Route("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel bag)
        {
            // if user inputs are valid, attempt registration
            if (ModelState.IsValid)
            {
                if (await _user.Register(bag))
                {
                    return RedirectToRoute("");
                }
            }

            // if invalid inputs, notify to try again
            ModelState.AddModelError(string.Empty, "Registration failed. Please try again.");
            return View("~/Views/Home/Register.cshtml", bag);
        }


        /// <summary>
        /// POST: Home/Login - 
        /// redirects to role-specific Dashboard if login successful, reloads login page with error messages and user inputs if unsuccessful
        /// </summary>
        /// <param name="bag"> login data submitted by user </param>
        /// <returns> redirect to user's dashboard if successful account creation, reloads Login if not </returns>
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel bag)
        {
            if (ModelState.IsValid)
            {
                var query = await _signInManager.PasswordSignInAsync(bag.Email, bag.Password, false, false);

                if (query.Succeeded)
                {
                    return RedirectToRoute("");
                }

            }
            ModelState.AddModelError(string.Empty, "Login failed. Please try again.");

            return View("~/Views/Home/Index.cshtml");
        }

        /// <summary>
        /// GET: Home/Logout - 
        /// logs out active user and returns to Home Login page
        /// </summary>
        /// <returns> redirects to Home/Login </returns>
        [HttpGet]
        [Route("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }


        // TODO: Add password reset support

        //[HttpGet]
        //[AllowAnonymous]
        //public async Task<IActionResult> PasswordReset()
        //{
        //    return View();
        //}


        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> PasswordReset(LoginViewModel bag)
        //{
        //    string token = await _userManager.GeneratePasswordResetTokenAsync(await _userManager.FindByEmailAsync(bag.Email));
        //    // email token
        //    if(token != null)
        //    {
        //        Email message = new Email()
        //        {
        //            Recipient = User.Identity.Name,
        //            ConfigSet = "",
        //            Subject = "Your Silton Foundation password reset request",
        //            BodyText = $@"<html>
        //                        <head></head>
        //                        <body>
        //                            <p>Your password reset token is:</p>
        //                            <p></p>
        //                            <p>{token}</p>
        //                            <p></p>
        //                        </body>
        //                        </html>",

        //        };
        //        bool emailStatus = await Email.Send(message);
        //    }
        //    return View("~/Home/PasswordReset.cshtml");
        //}

        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> ResetPassword(ChangePasswordViewModel bag)
        //{
        //    AppUser user = await _userManager.FindByEmailAsync(bag.Email);
        //    await _userManager.ResetPasswordAsync(user, bag.Token, bag.Password);
        //    user = await _userManager.FindByEmailAsync(bag.Email);
        //    await _signInManager.SignInAsync(user, isPersistent: false);
        //    return RedirectToRoute("");
        //}
    }
}
