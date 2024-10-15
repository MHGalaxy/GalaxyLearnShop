using System.ComponentModel.DataAnnotations;

namespace GalaxyLearn.Core.DTOs.CourseLevel
{
    public class EditCourseLevelViewModel
    {
        public int CourseLevelId { get; set; }

        [Display(Name = "نام (فارسی)")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string CourseLevelName { get; set; }

        [Display(Name = "نام (لاتین)")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string CourseLevelTitle { get; set; }
    }
}
