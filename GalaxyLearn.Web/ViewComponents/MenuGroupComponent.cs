using GalaxyLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GalaxyLearn.Web.ViewComponents
{
    public class MenuGroupComponent : ViewComponent
    {
        private readonly ICourseService _courseService;
        public MenuGroupComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _courseService.GetAllMenuGroups();
            return await Task.FromResult<IViewComponentResult>(View("MenuGroup", model));
        }
    }
}
