using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GalaxyLearn.DataLayer.Entities.Course
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required]
        public int CourseGroupId { get; set; }

        public int? CourseSubGroupId { get; set; }

        [Required]
        public int TeacherId { get; set; }

        [Required]
        public int CourseStatusId { get; set; }

        [Required]
        public int CourseLevelId { get; set; }

        [Display(Name = "عنوان دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CourseTitle { get; set; }

        [Display(Name = "شرح دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CourseDescription { get; set; }

        [Display(Name = "قیمت دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CoursePrice { get; set; }

        [MaxLength(1000)]
        public string Tags { get; set; }

        [MaxLength(50)]
        public string CourseImageName { get; set; }

        [MaxLength(100)]
        public string DemoFileName { get; set; }

        [Display(Name = "قفل است؟")]
        public bool IsLock { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }


        #region Relations

        [ForeignKey("TeacherId")]
        public User.User User { get; set; }

        public CourseGroup CourseGroup { get; set; }

        [ForeignKey("CourseSubGroupId")]
        public CourseGroup CourseSubGroup { get; set; }

        public CourseStatus CourseStatus { get; set; }

        public CourseLevel CourseLevel { get; set; }

        public List<CourseEpisode> CourseEpisodes { get; set; }
        #endregion
    }
}
