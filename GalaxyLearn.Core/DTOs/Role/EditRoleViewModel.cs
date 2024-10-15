using System.ComponentModel.DataAnnotations;

namespace GalaxyLearn.Core.DTOs.Role
{
    public class EditRoleViewModel
    {
        public int RoleId { get; set; }

        [Display(Name = "نام (فارسی)")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string RoleName { get; set; }

        [Display(Name = "نام (لاتین)")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string RoleTitle { get; set; }

        [Display(Name = "دسترسی ها")]
        public List<int>? PermissionIds { get; set; }
    }
}
