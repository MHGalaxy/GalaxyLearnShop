using AutoMapper;
using GalaxyLearn.Core.DTOs.UserPanel;
using GalaxyLearn.Core.Extensions;
using GalaxyLearn.Core.Services.Interfaces;
using GalaxyLearn.Core.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GalaxyLearn.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserPanelService _userPanelService;
        private readonly IWalletService _walletService;

        public HomeController(IMapper mapper, IUserPanelService userPanelService, IWalletService walletService)
        {
            _mapper = mapper;
            _userPanelService = userPanelService;
            _walletService = walletService;
        }

        public IActionResult Index()
        {
            return View();
        }



        #region Dashboard

        [Route("UserPanel/Dashboard")]
        public IActionResult Dashboard()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var user = _userPanelService.GetUserById(userId);
            var dashboard = _mapper.Map<UserDashboardViewModel>(user);
            dashboard.UserAcountBalance = _walletService.UserAccountBalance(userId);

            return View(dashboard);
        }

        #endregion

        #region EditProfile

        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var user = _userPanelService.GetUserById(userId);

            var profile = _mapper.Map<UserEditProfileViewModel>(user);

            var model = new UserProfileViewModel()
            {
                Profile = profile
            };

            return View(model);
        }

        [Route("UserPanel/EditProfile")]
        [HttpPost]
        public IActionResult EditProfile(UserEditProfileViewModel profile)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "اطلاعات ورودی معتبر نمی باشد" });
            }

            if (profile.UserImage != null)
            {
                if (!UploadFileHelper.AllowedImageTypes().ContainsValue(profile.UserImage.ContentType))
                {
                    return Json(new { success = false, message = "فرمت تصویر انتخابی باید JPG یا PNG باشد" });
                }

                if (!profile.UserImage.IsImage())
                {
                    return Json(new { success = false, message = "تصویر انتخابی معتبر نمی باشد" });
                }

                if (profile.UserImage.Length / 1024 / 1024 > 1)
                {
                    return Json(new { success = false, message = "حجم تصویر انتخابی باید کمتر از 1 مگابایت باشد" });
                }
            }

            _userPanelService.EditPorfile(userId, profile);

            return Json(new { success = true, message = "اطلاعات با موفقیت ذخیره شد" });
        }

        [HttpPost]
        [Route("UserPanel/DeleteImageProfile")]
        public IActionResult DeleteImageProfile()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            if (!_userPanelService.DeleteImageProfile(userId))
            {
                return Json(new { success = false, message = "تصویر پروفایلی وجود ندارد" });
            }

            return Json(new { success = true, message = "تصویر پروفایل با موفقیت حذف شد" });
        }


        [HttpPost]
        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordViewModel password)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "اطلاعات ورودی معتبر نمی باشد" });
            }

            if (!_userPanelService.ComparePassword(userId, password.OldPassword))
            {
                return Json(new { success = false, message = "رمز عبور فعلی صحیح نمی باشد" });
            }

            if (!_userPanelService.UpdatePassword(userId, password.Password))
            {
                return Json(new { success = false, message = "تغییر رمز عبور موفقیت آمیز نبود" });
            }

            return Json(new { success = true, message = "رمز عبور با موفقیت تغییر یافت" });
        }
        #endregion
    }
}
