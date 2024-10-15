using System.ComponentModel.DataAnnotations;

namespace GalaxyLearn.Core.DTOs.User
{
    public class ResetPasswordViewModel
    {
        public string ActiveCode { get; set; }

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
