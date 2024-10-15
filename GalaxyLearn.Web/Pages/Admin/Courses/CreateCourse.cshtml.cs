using AutoMapper;
using GalaxyLearn.Core.DTOs.Course;
using GalaxyLearn.Core.DTOs.CourseGroup;
using GalaxyLearn.Core.Services.Interfaces;
using GalaxyLearn.Core.Validators;
using GalaxyLearn.DataLayer.Entities.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GalaxyLearn.Web.Pages.Admin.Courses
{
    public class CreateCourseModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly ICourseService _courseService;

        public CreateCourseModel(IMapper mapper, ICourseService courseService)
        {
            _mapper = mapper;
            _courseService = courseService;
        }

        [BindProperty]
        public CreateCourseViewModel CreateCourseViewModel { get; set; }
        public void OnGet()
        {
            var groups = _courseService.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");

            var subGrous = new List<SelectListItem>();
            ViewData["SubGroups"] = new SelectList(subGrous, "Value", "Text");

            var teachers = _courseService.GetTeachers();
            ViewData["Teachers"] = new SelectList(teachers, "Value", "Text");

            var levels = _courseService.GetLevels();
            ViewData["Levels"] = new SelectList(levels, "Value", "Text");

            var statues = _courseService.GetStatues();
            ViewData["Statues"] = new SelectList(statues, "Value", "Text");
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, message = "اطلاعات ورودی معتبر نمی باشد" });
            }

            //Check Image File
            if (CreateCourseViewModel.CourseImageFile != null)
            {
                if (!UploadFileHelper.AllowedImageTypes().ContainsValue(CreateCourseViewModel.CourseImageFile.ContentType))
                {
                    return new JsonResult(new { success = false, message = "فرمت تصویر انتخابی باید JPG یا PNG باشد" });
                }

                if (CreateCourseViewModel.CourseImageFile.Length / 1024 / 1024 > 2)
                {
                    return new JsonResult(new { success = false, message = "حجم تصویر انتخابی باید کمتر از 2 مگابایت باشد" });
                }
            }

            //Check Video File
            if (CreateCourseViewModel.CourseDemoFile != null)
            {
                if (!UploadFileHelper.AllowedVideoTypes().ContainsValue(CreateCourseViewModel.CourseDemoFile.ContentType))
                {
                    return new JsonResult(new { success = false, message = "فرمت ویدیوی انتخابی باید mkv یا mp4 و یا avi باشد" });
                }

                if (CreateCourseViewModel.CourseDemoFile.Length / 1024 / 1024 > 100)
                {
                    return new JsonResult(new { success = false, message = "حجم ویدیوی انتخابی باید کمتر از 100 مگابایت باشد" });
                }
            }

            var courseId = _courseService.AddCourseFromAdmin(CreateCourseViewModel);

            return new JsonResult(new { success = true, message = "اطلاعات با موفقیت ذخیره شد" });
        }

        public IActionResult OnGetGetSubCourseGroup(int courseGroupId)
        {
            var list = new List<SelectListItem>()
            {
                new SelectListItem(){ Text = "انتخاب کنید (اختیاری)", Value = "" }
            };

            list.AddRange(_courseService.GetSubGroupForManageCourse(courseGroupId));
            return new JsonResult(new SelectList(list, "Value", "Text"));
        }
    }
}
