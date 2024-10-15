using AutoMapper;
using GalaxyLearn.Core.DTOs.CourseLevel;
using GalaxyLearn.Core.DTOs.Role;
using GalaxyLearn.Core.Services;
using GalaxyLearn.Core.Services.Interfaces;
using GalaxyLearn.DataLayer.Entities.Course;
using GalaxyLearn.DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GalaxyLearn.Web.Pages.Admin.CourseLevels
{
    public class EditCourseLevelModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly ICourseService _courseService;

        public EditCourseLevelModel(IMapper mapper, ICourseService courseService)
        {
            _mapper = mapper;
            _courseService = courseService;
        }

        [BindProperty]
        public EditCourseLevelViewModel EditCourseLevelViewModel { get; set; }

        public void OnGet(int courseLevelId)
        {
            var courseLevel = _courseService.GetCourseLevelById(courseLevelId);
            EditCourseLevelViewModel = _mapper.Map<EditCourseLevelViewModel>(courseLevel);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, message = "اطلاعات ورودی معتبر نمی باشد" });
            }

            //var courseLevel = _mapper.Map<CourseLevel>(EditRoleViewModel);
            //_permissionService.EditRole(role);

            return new JsonResult(new { success = true, message = "اطلاعات با موفقیت ذخیره شد" });
        }
    }
}
