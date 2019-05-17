using System;
using System.ComponentModel.DataAnnotations;

namespace SiltonFoundation.Models.ViewModels
{
    public class RegisterViewModel
    {
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

        [Required, StringLength(50, MinimumLength = 8, ErrorMessage = "Password is invalid.")]
        [Display(Name = "Password *")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [Display(Name = "Confirm Password *")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        // optional account properties
        [Display(Name = "Mailing Address")]
        public string MailAddress { get; set; }

        [Display(Name = "City/Town")]
        [StringLength(40)]
        public string MailCity { get; set; }

        [Display(Name = "State")]
        public State MailState { get; set; }

        [Display(Name = "Zip")]
        [RegularExpression(@"^[0-9]{5}$", ErrorMessage = "Zip code must be 5 digits.")]
        [StringLength(5, ErrorMessage = "Zip code must be 5 digits.")]
        public string MailZip { get; set; }

    }


}
