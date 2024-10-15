using System.ComponentModel.DataAnnotations;

namespace GalaxyLearn.Core.DTOs.CourseGroup
{
    public class EditCourseGroupViewModel
    {
        public int CourseGroupId { get; set; }

        [Display(Name = "نام (فارسی)")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string CourseGroupName { get; set; }

        [Display(Name = "نام (لاتین)")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string CourseGroupTitle { get; set; }

        [Display(Name = "گروه درسی پدر")]
        public int? ParentId { get; set; }
    }
}
