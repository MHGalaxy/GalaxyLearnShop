using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalaxyLearn.DataLayer.Entities.User
{
    public class User
    {
        public User()
        {

        }

        [Key]
        public int UserId { get; set; }

        [Display(Name = "نام ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { 
            get 
            {
                return FirstName + " " + LastName;
            }
        }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [StringLength(11, ErrorMessage = "{0} نباید از {1} رقم بیشتر باشد")]
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

        [Display(Name = "کد فعالسازی")]
        [MaxLength(50, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string ActiveCode { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Display(Name = "آواتار")]
        [MaxLength(200, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string UserAvatar { get; set; }

        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }


        #region Relations

        public List<UserRole> UserRoles { get; set; }
        public List<Wallet.Wallet> Wallets { get; set; }
        public List<Course.Course> Courses { get; set; }

        #endregion
    }
}
