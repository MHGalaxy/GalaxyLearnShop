using System.ComponentModel.DataAnnotations;

namespace GalaxyLearn.DataLayer.Entities.Course
{
    public class CourseStatus
    {
        [Key]
        public int CourseStatusId { get; set; }

        [Display(Name = "نام (فارسی)")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string CourseStatusName { get; set; }

        [Display(Name = "نام (لاتین)")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(100, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string CourseStatusTitle { get; set; }

        #region Relations

        public List<Course> Courses { get; set; }

        #endregion
    }
}
