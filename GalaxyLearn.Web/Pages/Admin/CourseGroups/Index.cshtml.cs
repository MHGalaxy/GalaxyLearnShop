using AutoMapper;
using GalaxyLearn.Core.DTOs.CourseGroup;
using GalaxyLearn.Core.Enums;
using GalaxyLearn.Core.Security;
using GalaxyLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GalaxyLearn.Web.Pages.Admin.CourseGroups
{
    //[PermissionChecker((int)PermissionEnum.RoleManagement)]
    public class IndexModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly ICourseService _courseService;
        public IndexModel(IMapper mapper, ICourseService courseService)
        {
            _mapper = mapper;
            _courseService = courseService;
        }

        public List<CourseGroupViewModel> CourseGroups { get; set; }

        public void OnGet()
        {
            var courseGroups = _courseService.GetAllCourseGroups();
            CourseGroups = _mapper.Map<List<CourseGroupViewModel>>(courseGroups);
        }

        public IActionResult OnGetDeleteCourseGroup(int courseGroupId)
        {
            _courseService.DeleteCourseGroup(courseGroupId);
            return new JsonResult(new { success = true, message = "گروه درسی با موفقیت حذف شد" });
        }
    }
}
