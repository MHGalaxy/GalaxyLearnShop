using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GalaxyLearn.DataLayer.Entities.Course
{
    public class CourseGroup
    {
        [Key]
        public int CourseGroupId { get; set; }

        [Display(Name = "نام (فارسی)")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string CourseGroupName { get; set; }

        [Display(Name = "نام (لاتین)")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        [MaxLength(200, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string CourseGroupTitle { get; set; }

        [Display(Name = "گروه اصلی")]
        public int? ParentId { get; set; }


        #region Relations

        [ForeignKey("ParentId")]
        public List<CourseGroup> CourseGroups { get; set; }

        [InverseProperty("CourseGroup")]
        public List<Course> Courses { get; set; }

        [InverseProperty("CourseSubGroup")]
        public List<Course> SubGroupCourses { get; set; }
        #endregion
    }
}
