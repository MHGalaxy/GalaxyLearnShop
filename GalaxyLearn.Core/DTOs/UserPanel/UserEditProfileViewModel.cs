using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace GalaxyLearn.Core.DTOs.UserPanel
{
    public class UserEditProfileViewModel
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
        [StringLength(11, ErrorMessage = "{0} نباید از {1} رقم بیشتر باشد")]
        [RegularExpression(@"^09\d{9}$", ErrorMessage = "شماره تلفن وارد شده معتبر نمی باشد")]
        public string PhoneNumber { get; set; }

        [Display(Name = "ایمیل")]
        [MaxLength(200, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string? UserEmail { get; set; }

        [Display(Name = "آواتار")]
        [MaxLength(200, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string UserAvatar { get; set; }

        
        public IFormFile? UserImage { get; set; }

    }
}
