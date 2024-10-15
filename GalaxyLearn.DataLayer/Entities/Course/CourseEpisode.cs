using System.ComponentModel.DataAnnotations;

namespace GalaxyLearn.DataLayer.Entities.Course
{
    public class CourseEpisode
    {
        [Key]
        public int EpisodeId { get; set; }

        public int CourseId { get; set; }

        [Display(Name = "عنوان قسمت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string EpisodeTitle { get; set; }

        [Display(Name = "طول زمان دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public TimeSpan EpisodeTime { get; set; }

        [Display(Name = "فایل")]
        public string? EpisodeFileName { get; set; }

        [Display(Name = "رایگان")]
        public bool IsFree { get; set; }


        #region Relations

        public Course Course { get; set; }

        #endregion
    }
}
