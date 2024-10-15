using AutoMapper;
using GalaxyLearn.Core.DTOs.Role;
using GalaxyLearn.Core.Enums;
using GalaxyLearn.Core.Security;
using GalaxyLearn.Core.Services.Interfaces;
using GalaxyLearn.DataLayer.Entities.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GalaxyLearn.Web.Pages.Admin.Roles
{
    [PermissionChecker((int)PermissionEnum.AddRole)]
    public class CreateRoleModel : PageModel
    {
        private readonly IUserAdminPanelService _userAdminPanelService;
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;

        public CreateRoleModel(IUserAdminPanelService userAdminPanelService, IPermissionService persmissionService, IMapper mapper)
        {
            _userAdminPanelService = userAdminPanelService;
            _permissionService = persmissionService;
            _mapper = mapper;
        }

        [BindProperty]
        public CreateRoleViewModel CreateRoleViewModel { get; set; }

        public void OnGet()
        {
            ViewData["Permissions"] =_permissionService.GetAllPermissions();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, message = "اطلاعات ورودی معتبر نمی باشد" });
            }

            var role = _mapper.Map<Role>(CreateRoleViewModel);
            var roleId = _permissionService.AddRole(role);
            _permissionService.AddPermissionsToRole(roleId, CreateRoleViewModel.PermissionIds);

            return new JsonResult(new { success = true, message = "اطلاعات با موفقیت ذخیره شد" });
        }
    }
}
