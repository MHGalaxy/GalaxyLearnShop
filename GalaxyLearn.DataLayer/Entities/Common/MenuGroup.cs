using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GalaxyLearn.DataLayer.Entities.Common
{
    public class MenuGroup
    {
        [Key]
        public int MenuGroupId { get; set; }

        [Display(Name = "نام (فارسی)")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string MenuGroupName { get; set; }

        [Display(Name = "نام (لاتین)")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string MenuGroupTitle { get; set; }

        [Display(Name = "گروه اصلی")]
        public int? ParentId { get; set; }

        [Display(Name = "آدرس منو")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string Link { get; set; } = string.Empty;

        [Display(Name = "ترتیب")]
        public int Order { get; set; }


        #region Relations

        [ForeignKey("ParentId")]
        public List<MenuGroup> MenuGroups { get; set; }

        #endregion
    }
}
