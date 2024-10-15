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
    [PermissionChecker((int)PermissionEnum.EditRole)]
    public class EditRoleModel : PageModel
    {
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;
        public EditRoleModel(IPermissionService permissionService, IMapper mapper)
        {
            _permissionService = permissionService;
            _mapper = mapper;
        }

        [BindProperty]
        public EditRoleViewModel EditRoleViewModel { get; set; }
        public void OnGet(int roleId)
        {
            ViewData["Permissions"] = _permissionService.GetAllPermissions();
            EditRoleViewModel = _permissionService.GetRoleForEdit(roleId);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, message = "اطلاعات ورودی معتبر نمی باشد" });
            }

            var role = _mapper.Map<Role>(EditRoleViewModel);
            _permissionService.EditRole(role);
            _permissionService.EditPermissionToRole(role.RoleId, EditRoleViewModel.PermissionIds);

            return new JsonResult(new { success = true, message = "اطلاعات با موفقیت ذخیره شد" });
        }
    }
}
