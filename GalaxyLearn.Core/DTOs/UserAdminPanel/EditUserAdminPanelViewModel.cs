using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalaxyLearn.Core.DTOs.UserAdminPanel
{
    public class EditUserAdminPanelViewModel
    {
        public int UserId { get; set; }

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
        [MaxLength(200, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string? Password { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; }

        [Display(Name = "آواتار")]
        [MaxLength(200, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string UserAvatar { get; set; }
        public IFormFile? UserImage { get; set; }

        [Display(Name = "نقش ها")]
        [Required(ErrorMessage = "حداقل یک نقش را انتخاب کنید")]
        public List<int> RoleIds { get; set; }
    }
}
