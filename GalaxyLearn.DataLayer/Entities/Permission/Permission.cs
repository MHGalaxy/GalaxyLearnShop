using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GalaxyLearn.DataLayer.Entities.Permission
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }

        [Display(Name = "نام (فارسی)")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string PermissionName { get; set; }

        [Display(Name = "نام (لاتین)")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string PermissionTitle { get; set; }

        public int? ParentId { get; set; }

        #region Relations

        [ForeignKey("ParentId")]
        public List<Permission> Permissions { get; set; }

        public List<RolePermission> RolePermissions { get; set; }

        #endregion
    }
}
