using AutoMapper;
using GalaxyLearn.Core.DTOs.Course;
using GalaxyLearn.Core.Services;
using GalaxyLearn.Core.Services.Interfaces;
using GalaxyLearn.Core.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GalaxyLearn.Web.Pages.Admin.Courses
{
    public class EditCourseModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly ICourseService _courseService;

        public EditCourseModel(IMapper mapper, ICourseService courseService)
        {
            _mapper = mapper;
            _courseService = courseService;
        }

        [BindProperty]
        public EditCourseViewModel EditCourseViewModel { get; set; }

        public void OnGet(int courseId)
        {
            var course = _courseService.GetCourseById(courseId);
            EditCourseViewModel = _mapper.Map<EditCourseViewModel>(course);


            var groups = _courseService.GetGroupForManageCourse();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");

            var subGrous = _courseService.GetSubGroupForManageCourse(course.CourseGroupId);
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
            if (EditCourseViewModel.CourseImageFile != null)
            {
                if (!UploadFileHelper.AllowedImageTypes().ContainsValue(EditCourseViewModel.CourseImageFile.ContentType))
                {
                    return new JsonResult(new { success = false, message = "فرمت تصویر انتخابی باید JPG یا PNG باشد" });
                }

                if (EditCourseViewModel.CourseImageFile.Length / 1024 / 1024 > 2)
                {
                    return new JsonResult(new { success = false, message = "حجم تصویر انتخابی باید کمتر از 2 مگابایت باشد" });
                }
            }

            //Check Video File
            if (EditCourseViewModel.CourseDemoFile != null)
            {
                if (!UploadFileHelper.AllowedVideoTypes().ContainsValue(EditCourseViewModel.CourseDemoFile.ContentType))
                {
                    return new JsonResult(new { success = false, message = "فرمت ویدیوی انتخابی باید mkv یا mp4 و یا avi باشد" });
                }

                if (EditCourseViewModel.CourseDemoFile.Length / 1024 / 1024 > 100)
                {
                    return new JsonResult(new { success = false, message = "حجم ویدیوی انتخابی باید کمتر از 100 مگابایت باشد" });
                }
            }

            var courseId =_courseService.EditCourseForAdmin(EditCourseViewModel);

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

        public IActionResult OnGetDeleteCourseImage(int courseId)
        {
            if (!_courseService.DeleteCourseImage(courseId))
            {
                return new JsonResult(new { success = false, message = "تصویر دوره ای وجود ندارد" });
            }

            return new JsonResult(new { success = true, message = "تصویر دوره با موفقیت حذف شد" });
        }

        public IActionResult OnGetDeleteCourseDemo(int courseId)
        {
            if (!_courseService.DeleteCourseDemo(courseId))
            {
                return new JsonResult(new { success = false, message = "دموی دوره ای وجود ندارد" });
            }

            return new JsonResult(new { success = true, message = "دموی دوره با موفقیت حذف شد" });
        }
    }
}
