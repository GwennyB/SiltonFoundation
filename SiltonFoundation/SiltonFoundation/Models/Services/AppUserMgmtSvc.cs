﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiltonFoundation.Models.Interfaces;
using SiltonFoundation.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SiltonFoundation.Models.Services
{
    // The respository design pattern is implemented throughout this app in order to add a layer of abstraction between the client and the data stores, thereby protecting the data stores from certain types of attacks and breaches.
    // Dependency injection is the means by which the RDP is implemented in this app, which offers the added benefit of ease of maintenance.
    // This service implements the IAppUserManager interface and provides all functionality that touches the 'AppUsers' table in the SiltonUser database. This service also injects and uses MS Identity's UserManager and SignInManager interfaces.
    // This service is injected into controllers throughout this app in order to facilitate their access to this table (rather than accessing the DB directly).
    public class AppUserMgmtSvc : IAppUserManager
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;

        public AppUserMgmtSvc(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        /// <summary>
        /// Updates user profile and returns updated (if successful) or requested (if unsuccessful) user details
        /// </summary>
        /// <param name="bag"> requested user updates </param>
        /// <returns> updated or requested user details with update failure report added </returns>
        public async Task<UpdateProfileViewModel> UpdateUserProfile(UpdateProfileViewModel bag)
        {
            AppUser user = await BuildUserUpdate(bag);

            // update profile data in DB
            var query = await _userManager.UpdateAsync(user);

            // change password if entered and validated
            if (bag.OldPassword != null && bag.NewPassword != null)
            {
                await _userManager.ChangePasswordAsync(user, bag.OldPassword, bag.NewPassword);
            }

            // replace claims
            if (query.Succeeded)
            {
                await _userManager.RemoveClaimsAsync(user, await _userManager.GetClaimsAsync(user));

                Claim fullNameClaim = new Claim("FullName", $"{user.FirstName} {user.LastName}");
                Claim email = new Claim(ClaimTypes.Email, bag.Email, ClaimValueTypes.Email);
                await _userManager.AddClaimsAsync(user, new List<Claim> { fullNameClaim, email });
                bag = await BuildUPVM(bag.Email);
                bag.UpdateFailed = false;

                return bag;
            }
            else
            {
                bag.UpdateFailed = true;
            }
            return bag;
        }

        /// <summary>
        /// HELPER: Builds an AppUser object from an UpdateProfileViewModel object
        /// </summary>
        /// <param name="bag"> UpdateProfileViewModel object from which to build AppUser object </param>
        /// <returns> AppUser object built from properties in param 'bag' </returns>
        public async Task<AppUser> BuildUserUpdate (UpdateProfileViewModel bag)
        {
            AppUser user = await _userManager.FindByEmailAsync(bag.Email);

            user.UserName = bag.Email.ToLower();
            user.Email = bag.Email.ToLower();
            user.FirstName = bag.FirstName;
            user.LastName = bag.LastName;
            user.Birthdate = bag.Birthdate;
            user.PhoneNumber = bag.Phone;
            user.MailAddress = bag.MailAddress;
            user.MailCity = bag.MailCity;
            user.MailState = bag.MailState;
            user.MailZip = bag.MailZip;

            return user;
        }

        /// <summary>
        /// HELPER: Builds an UpdateProfileViewModel object from an AppUser object pulled from database using provided email
        /// </summary>
        /// <param name="bag"> email for AppUser object to locate in DB </param>
        /// <returns> UpdateProfileViewModel object, or 'null' if user not found by provided email </returns>
        public async Task<UpdateProfileViewModel> BuildUPVM (string email)
        {
            AppUser appUser = await _userManager.FindByEmailAsync(email);
            if (appUser != null)
            {
                UpdateProfileViewModel upvm = new UpdateProfileViewModel()
                {
                    FirstName = appUser.FirstName,
                    LastName = appUser.LastName,
                    Birthdate = appUser.Birthdate,
                    Email = appUser.Email,
                    Phone = appUser.PhoneNumber,
                    MailAddress = appUser.MailAddress,
                    MailCity = appUser.MailCity,
                    MailState = appUser.MailState,
                    MailZip = appUser.MailZip
                };
            return upvm;
            }
            return null;
        }


    }
}