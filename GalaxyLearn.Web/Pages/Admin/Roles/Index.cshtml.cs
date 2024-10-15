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
    [PermissionChecker((int)PermissionEnum.RoleManagement)]
    public class IndexModel : PageModel
    {
        private readonly IPermissionService _permissionService;
        private readonly IMapper _mapper;
        public IndexModel(IPermissionService permissionService, IMapper mapper)
        {
            _permissionService = permissionService;
            _mapper = mapper;
        }

        public List<RoleViewModel> Roles { get; set; }

        public void OnGet()
        {
            var roles = _permissionService.GetRoles();
            Roles = _mapper.Map<List<RoleViewModel>>(roles);
        }

        public IActionResult OnGetDeleteRole(int roleId)
        {
            _permissionService.DeleteRole(roleId);
            return new JsonResult(new { success = true, message = "نقش با موفقیت حذف شد" });
        }
    }
}
