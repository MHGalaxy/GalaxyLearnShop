using GalaxyLearn.Core.DTOs.UserAdminPanel;
using GalaxyLearn.Core.Enums;
using GalaxyLearn.Core.Security;
using GalaxyLearn.Core.Services;
using GalaxyLearn.Core.Services.Interfaces;
using GalaxyLearn.Core.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace GalaxyLearn.Web.Pages.Admin.Users
{
    [PermissionChecker((int)PermissionEnum.EditUser)]
    public class EditUserModel : PageModel
    {
        private readonly IUserAdminPanelService _userAdminPanelService;
        private readonly IPermissionService _permissionService;
        private readonly IUserPanelService _userPanelService;

        public EditUserModel(IUserAdminPanelService userAdminPanelService, IPermissionService permissionService, IUserPanelService userPanelService)
        {
            _userAdminPanelService = userAdminPanelService;
            _permissionService = permissionService;
            _userPanelService = userPanelService;
        }

        [BindProperty]
        public EditUserAdminPanelViewModel EditUserViewModel { get; set; }

        public void OnGet(int userId)
        {
            ViewData["Roles"] = _permissionService.GetRoles();
            EditUserViewModel = _userAdminPanelService.GetUserForEdit(userId);
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, message = "اطلاعات ورودی معتبر نمی باشد" });
            }

            if (EditUserViewModel.UserImage != null)
            {
                if (!UploadFileHelper.AllowedFileTypes().ContainsValue(EditUserViewModel.UserImage.ContentType))
                {
                    return new JsonResult(new { success = false, message = "فرمت تصویر انتخابی باید JPG یا PNG باشد" });
                }

                if (EditUserViewModel.UserImage.Length / 1024 / 1024 > 1)
                {
                    return new JsonResult(new { success = false, message = "حجم تصویر انتخابی باید کمتر از 1 مگابایت باشد" });
                }
            }

            _userAdminPanelService.EditUserForAdmin(EditUserViewModel);
            _permissionService.EditUserRoles(EditUserViewModel.RoleIds, EditUserViewModel.UserId);

            return new JsonResult(new { success = true, message = "اطلاعات با موفقیت ذخیره شد" });
        }

        public IActionResult OnGetDeleteImage(int userId)
        {
            if (!_userPanelService.DeleteImageProfile(userId))
            {
                return new JsonResult(new { success = false, message = "تصویر پروفایلی وجود ندارد" });
            }

            return new JsonResult(new { success = true, message = "تصویر پروفایل با موفقیت حذف شد" });
        }
    }
}
