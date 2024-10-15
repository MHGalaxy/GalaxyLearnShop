using GalaxyLearn.Core.DTOs.UserAdminPanel;
using GalaxyLearn.Core.Enums;
using GalaxyLearn.Core.Security;
using GalaxyLearn.Core.Services;
using GalaxyLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GalaxyLearn.Web.Pages.Admin.Users
{
    [PermissionChecker((int)PermissionEnum.UserManagement)]
    public class IndexModel : PageModel
    {
        private readonly IUserAdminPanelService _userAdminPanelService;
        private readonly IUserPanelService _userPanelService;
        public IndexModel(IUserAdminPanelService userAdminPanelService, IUserPanelService userPanelService)
        {
            _userAdminPanelService = userAdminPanelService;
            _userPanelService = userPanelService;
        }

        public List<UserAdminPanelViewModel> Users { get; set; }

        public void OnGet()
        {
            Users = _userAdminPanelService.GetAllUsers();
        }

        public IActionResult OnGetDeleteUser(int userId)
        {
            _userPanelService.DeleteImageProfile(userId);
            _userAdminPanelService.DeleteUser(userId);
            return new JsonResult(new { success = true, message = "کاربر با موفقیت حذف شد" });
        }
    }
}
