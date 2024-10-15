using GalaxyLearn.DataLayer.Entities.Permission;
using System.ComponentModel.DataAnnotations;

namespace GalaxyLearn.DataLayer.Entities.User
{
    public class Role
    {
        public Role()
        {

        }

        [Key]
        public int RoleId { get; set; }

        [Display(Name = "نام (فارسی)")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string RoleName { get; set; }

        [Display(Name = "نام (لاتین)")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string RoleTitle { get; set; }

        #region Relations

        public virtual List<UserRole> UserRoles { get; set; }
        public virtual List<RolePermission> RolePermissions { get; set; }

        #endregion
    }
}
