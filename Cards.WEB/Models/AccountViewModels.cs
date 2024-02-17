using System.ComponentModel.DataAnnotations;

namespace Cards.WEB.Models
{
    public class AccountViewModels
    {
        public class AccountLoginModel
        {
            [Required(ErrorMessage = "The Username is required")]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            public string ReturnUrl { get; set; }
            public bool RememberMe { get; set; }
        }

        public class AccountForgotPasswordModel
        {
            [Required(ErrorMessage = "Email Address is required")]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = "The Username is required")]
            public string UserName { get; set; }

            public string ReturnUrl { get; set; }
        }

        public class AccountResetPasswordModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password")]
            public string PasswordConfirm { get; set; }
        }

        public class AccountRegistrationModel
        {
            [Display(Name = "Username")]
            public string Username { get; set; }

            [Display(Name = "Mobile Number")]
            [Required]
            public string MobileNumber { get; set; }

            [Display(Name = "First Name")]
            [Required]
            public string FirstName { get; set; }

            [Display(Name = "Other Names")]
            [Required]
            public string OtherNames { get; set; }

            [Display(Name = "Company Name")]
            [Required]
            public string CompanyName { get; set; }

            [Display(Name = "Email")]
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Display(Name = "Email Confirm")]
            [Required]
            [EmailAddress]
            [Compare("Email")]
            public string EmailConfirm { get; set; }

            [Display(Name = "Password")]
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Password Confirm")]
            [Required]
            [DataType(DataType.Password)]
            [Compare("Password")]
            public string PasswordConfirm { get; set; }
        }

        public class AccountLockScreenModel
        {

            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            public string ReturnUrl { get; set; }
            public bool RememberMe { get; set; }
        }
    }
}
