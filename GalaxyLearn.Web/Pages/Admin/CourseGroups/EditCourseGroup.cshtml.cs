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
    //[PermissionChecker((int)PermissionEnum.EditRole)]
    public class EditCourseGroupModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly ICourseService _courseService;
        public EditCourseGroupModel(IMapper mapper, ICourseService courseService)
        {
            _mapper = mapper;
            _courseService = courseService;
        }

        [BindProperty]
        public EditCourseGroupViewModel EditCourseGroupViewModel { get; set; }
        public void OnGet(int courseGroupId)
        {
            ViewData["Parents"] = _courseService.GetAllParentCourseGroups();
            var courseGroup = _courseService.GetCourseGroupById(courseGroupId);
            EditCourseGroupViewModel = _mapper.Map<EditCourseGroupViewModel>(courseGroup);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, message = "اطلاعات ورودی معتبر نمی باشد" });
            }

            var courseGroup = _mapper.Map<CourseGroup>(EditCourseGroupViewModel);
            _courseService.EditCourseGroup(courseGroup);

            return new JsonResult(new { success = true, message = "اطلاعات با موفقیت ذخیره شد" });
        }
    }
}
