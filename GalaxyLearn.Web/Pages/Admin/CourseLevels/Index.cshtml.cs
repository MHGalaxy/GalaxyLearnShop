using AutoMapper;
using GalaxyLearn.Core.DTOs.CourseLevel;
using GalaxyLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GalaxyLearn.Web.Pages.Admin.CourseLevels
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


        public List<CourseLevelViewModel> CourseLevels { get; set; }

        public void OnGet()
        {
            var courseLevels = _courseService.GetAllCourseLevels();
            CourseLevels = _mapper.Map<List<CourseLevelViewModel>>(courseLevels);
        }

        public IActionResult OnGetDeleteCourseLevel(int courseLevelId)
        {
            _courseService.DeleteCourseLevel(courseLevelId);
            return new JsonResult(new { success = true, message = "سطح دوره با موفقیت حذف شد" });
        }
    }
}
