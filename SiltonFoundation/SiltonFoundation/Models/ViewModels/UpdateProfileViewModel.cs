using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace SiltonFoundation.Models.ViewModels
{
    public class UpdateProfileViewModel
    {
        public bool UpdateFailed { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First Name *")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name *")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Birthdate is required.")]
        [Display(Name = "Birthdate *")]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        [Display(Name = "E-Mail *")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [Phone]
        [Display(Name = "Phone *")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        // optional account properties
        [Display(Name = "Mailing Address")]
        public string MailAddress { get; set; }

        [Display(Name = "City/Town")]
        [MaxLength(40)]
        public string MailCity { get; set; }

        [Display(Name = "State")]
        public State MailState { get; set; }

        [Display(Name = "Zip")]
        [RegularExpression(@"^[0-9]{5}$", ErrorMessage = "Zip code must be 5 digits.")]
        public string MailZip { get; set; }


        // password changes
        [Display(Name = "Old Password")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm New Password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string ConfirmNewPassword { get; set; }

        public bool AdminUpdate { get; set; }

    }


}
