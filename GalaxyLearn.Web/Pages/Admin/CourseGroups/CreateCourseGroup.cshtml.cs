using AutoMapper;
using GalaxyLearn.Core.DTOs.CourseGroup;
using GalaxyLearn.Core.Enums;
using GalaxyLearn.Core.Security;
using GalaxyLearn.Core.Services.Interfaces;
using GalaxyLearn.DataLayer.Entities.Course;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GalaxyLearn.Web.Pages.Admin.CourseGroups
{
    //[PermissionChecker((int)PermissionEnum.AddRole)]
    public class CreateCourseGroupModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly ICourseService _courseService;

        public CreateCourseGroupModel(IMapper mapper, ICourseService courseService)
        {
            _mapper = mapper;
            _courseService = courseService;
        }

        [BindProperty]
        public CreateCourseGroupViewModel CreateCourseGroupViewModel { get; set; }

        public void OnGet()
        {
            ViewData["Parents"] = _courseService.GetAllParentCourseGroups();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, message = "اطلاعات ورودی معتبر نمی باشد" });
            }

            var courseGroup = _mapper.Map<CourseGroup>(CreateCourseGroupViewModel);
            var courseGroupId = _courseService.AddCourseGroup(courseGroup);

            return new JsonResult(new { success = true, message = "اطلاعات با موفقیت ذخیره شد" });
        }
    }
}
