using System.ComponentModel.DataAnnotations;

namespace GalaxyLearn.Core.DTOs.User
{
    public class RegisterViewModel
    {
        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string LastName { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(11, ErrorMessage = "{0} نباید از {1} رقم بیشتر باشد")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره تلفن وارد شده معتبر نمی باشد")]
        public string PhoneNumber { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string? UserEmail { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string Password { get; set; }

        [Display(Name = "تکرار کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [Compare("Password", ErrorMessage = "کلمه عبور یکسان نیست")]
        [MaxLength(200, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string ConfirmedPassword { get; set; }
    }
}
