using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GalaxyLearn.Core.DTOs.Course
{
    public class CreateCourseViewModel
    {
        [Display(Name = "گروه درسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CourseGroupId { get; set; }

        [Display(Name = "زیر گروه درسی")]
        public int? CourseSubGroupId { get; set; }

        [Display(Name = "مدرس")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int TeacherId { get; set; }

        [Display(Name = "وضعیت دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CourseStatusId { get; set; }

        [Display(Name = "سطح دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CourseLevelId { get; set; }

        [Display(Name = "عنوان دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(450, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string CourseTitle { get; set; }

        [Display(Name = "شرح دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string CourseDescription { get; set; }

        [Display(Name = "قیمت (به تومان)")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CoursePrice { get; set; }

        [Display(Name = "برچسب ها")]
        [MaxLength(1000)]
        public string? Tags { get; set; }

        [Display(Name = "تصویر دوره")]
        public IFormFile? CourseImageFile { get; set; }

        [Display(Name = "فایل دمو")]
        public IFormFile? CourseDemoFile { get; set; }

        [Display(Name = "قفل است؟")]
        public bool IsLock { get; set; }
    }
}
