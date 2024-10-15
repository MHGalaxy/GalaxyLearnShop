using AutoMapper;
using GalaxyLearn.Core.DTOs.Course;
using GalaxyLearn.Core.Services;
using GalaxyLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GalaxyLearn.Web.Pages.Admin.Courses
{
    public class IndexModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly ICourseService _courseService;

        public IndexModel(IMapper mapper, ICourseService courseService)
        {
            _mapper = mapper;
            _courseService = courseService;
        }

        public List<CourseViewModel> Courses { get; set; }
        public void OnGet()
        {
            Courses = _courseService.GetAllCoursesForAdmin();
        }

        public IActionResult OnGetDeleteCourse(int courseId)
        {
            _courseService.DeleteCourse(courseId);
            return new JsonResult(new { success = true, message = "دوره با موفقیت حذف شد" });
        }
    }
}
