using System.ComponentModel.DataAnnotations;

namespace GalaxyLearn.Core.DTOs.User
{
    public class LoginViewModel
    {
        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [StringLength(11, ErrorMessage = "{0} نباید از {1} رقم بیشتر باشد")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره تلفن وارد شده معتبر نمی باشد")]
        public string PhoneNumber { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool IsRemember { get; set; }
    }
}
