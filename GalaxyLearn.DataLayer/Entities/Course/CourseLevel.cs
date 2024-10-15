using System.ComponentModel.DataAnnotations;

namespace GalaxyLearn.DataLayer.Entities.Course
{
    public class CourseLevel
    {
        [Key]
        public int CourseLevelId { get; set; }

        [Display(Name = "نام (فارسی)")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string CourseLevelName { get; set; }

        [Display(Name = "نام (لاتین)")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string CourseLevelTitle { get; set; }

        #region Relations

        public List<Course> Courses { get; set; }

        #endregion
    }
}
