using AutoMapper;
using GalaxyLearn.Core.DTOs.UserAdminPanel;
using GalaxyLearn.Core.Enums;
using GalaxyLearn.Core.Security;
using GalaxyLearn.Core.Services.Interfaces;
using GalaxyLearn.Core.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GalaxyLearn.Web.Pages.Admin.Users
{
    [PermissionChecker((int)PermissionEnum.AddUser)]
    public class CreateUserModel : PageModel
    {
        private readonly IUserAdminPanelService _userAdminPanelService;
        private readonly IPermissionService _permissionService;

        public CreateUserModel(IUserAdminPanelService userAdminPanelService, IPermissionService persmissionService)
        {
            _userAdminPanelService = userAdminPanelService;
            _permissionService = persmissionService;
        }

        [BindProperty]
        public CreateUserAdminPanelViewModel CreateUserViewModel { get; set; }

        public void OnGet()
        {
            ViewData["Roles"] = _permissionService.GetRoles();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { success = false, message = "اطلاعات ورودی معتبر نمی باشد" });
            }

            if (CreateUserViewModel.UserImage != null)
            {
                if (!UploadFileHelper.AllowedFileTypes().ContainsValue(CreateUserViewModel.UserImage.ContentType))
                {
                    return new JsonResult(new { success = false, message = "فرمت تصویر انتخابی باید JPG یا PNG باشد" });
                }

                if (CreateUserViewModel.UserImage.Length / 1024 / 1024 > 1)
                {
                    return new JsonResult(new { success = false, message = "حجم تصویر انتخابی باید کمتر از 1 مگابایت باشد" });
                }
            }

            var userId = _userAdminPanelService.AddUserFromAdmin(CreateUserViewModel);

            _permissionService.AddRolesToUser(CreateUserViewModel.RoleIds, userId);

            return new JsonResult(new { success = true, message = "اطلاعات با موفقیت ذخیره شد" });
        }
    }
}
