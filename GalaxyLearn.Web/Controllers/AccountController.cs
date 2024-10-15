using GalaxyLearn.Core.DTOs.User;
using GalaxyLearn.Core.Generators;
using GalaxyLearn.Core.Security;
using GalaxyLearn.Core.Services.Interfaces;
using GalaxyLearn.DataLayer.Entities.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using GalaxyLearn.Core.Senders;
using GalaxyLearn.Core;
using GalaxyLearn.Core.Convertors;
using Microsoft.JSInterop.Implementation;

namespace GalaxyLearn.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISmsSevice _smsSevice;
        private readonly IEmailService _emailService;
        private readonly IPermissionService _permissionService;

        public AccountController(IUserService userService,
                                 ISmsSevice smsSevice,
                                 IEmailService emailService,
                                 IPermissionService permissionService)
        {
            _userService = userService;
            _smsSevice = smsSevice;
            _emailService = emailService;
            _permissionService = permissionService;
        }


        #region Register

        [Route("SignIn")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("SignIn")]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "اطلاعات ورودی معتبر نمی باشد" });
            }

            if (_userService.PhoneNumberExist(register.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "نام کاربری قبلا استفاده شده است");
                return Json(new { success = false, message = "این شماره موبایل قبلا ثبت نام کرده است" });
            }

            if (_userService.EmailExists(register.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "نام کاربری قبلا استفاده شده است");
                return Json(new { success = false, message = "این شماره موبایل قبلا ثبت نام کرده است" });
            }

            var user = new User()
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                ActiveCode = NameGenerator.GenerateUniqueCode(),
                IsActive = false,
                Password = PasswordHelper.EncodePasswordSha256(register.Password),
                RegisterDate = DateTime.UtcNow,
                PhoneNumber = register.PhoneNumber.Trim(),
                UserEmail = FixText.FixEmail(register.UserEmail),
                UserAvatar = "user-avatar-default.png",
            };

            var userId = _userService.AddUser(user);
            _permissionService.AddNormalUserRole(userId);

            return Json(new { success = true, message = user.FullName + " عزیز" + "\n ثبت نام شما با موفقیت انجام گردید", activeCode = user.ActiveCode });
        }

        #endregion

        #region Verification Register Code

        [HttpPost]
        public IActionResult SendRegisterSms(string activeCode)
        {
            var user = _userService.GetUserByActiveCode(activeCode);

            if (user != null && user.IsActive == false)
            {
                var tempCode = new TempCode()
                {
                    UserId = user.UserId,
                    Code = NameGenerator.GenerateRandomFourDigitNumber(),
                    SendCodeDateTime = DateTime.Now,
                    Type = (byte)TempCodeType.Register
                };
                _userService.AddTempCode(tempCode);

                var message = "آکادمی آموزشی فاطمه کهکشان. " +
                              "\n" + user.FullName + "عزیز به مجموعه ما خوش آمدید. " +
                              "کد یکبار مصرف تایید ثبت نام شما: " +
                              tempCode.Code;

                _smsSevice.SendSms(user.PhoneNumber, message);
                //_ = _emailService.SendEmailAsync(user.UserEmail, "کد تایید شماره موبایل", message);

                return Json(new { success = true, message = "کد تایید به شماره موبایل شما ارسال شد" });
            }

            return Json(new { success = false, message = "کد تایید به شماره موبایل شما ارسال نشد! دوباره امتحان کنید" });
        }


        [Route("[controller]/[action]/{activeCode}")]
        public IActionResult VerificationRegisterCode(string activeCode)
        {
            var user = _userService.GetUserByActiveCode(activeCode);

            if (user != null && user.IsActive == false)
            {
                return View(user);
            }

            return View("Register");
        }

        [HttpPost]
        public IActionResult VerificationRegisterCode(int userId, string tempCode)
        {
            if (!_userService.ValidateRegisterTempCode(userId, tempCode))
            {
                return Json(new { success = false, message = "کد تایید صحیح نمی باشد یا منقضی شده است" });

            }

            if (!_userService.ActivateUser(userId))
            {
                return Json(new { success = false, message = "در فرآیند تایید شماره موبایل مشکلی بوجود آمده است" });
            }

            return Json(new { success = true, message = "شماره موبایل شما تایید شد و اکانت شما فعال گردید" });
        }

        #endregion

        #region Login

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginViewModel login)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Json(new { success = false, message = "شما از قبل در حساب کاربری خود وارد شده اید" });
            }

            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "اطلاعات ورودی معتبر نمی باشند" });
            }

            var user = _userService.GetUserForLoginByPassword(login);

            if (user is null)
            {
                return Json(new { success = false, message = "کاربری با مشخصات وارد شده یافت نشد" });
            }

            if (!user.IsActive)
            {
                return Json(new { success = false, message = "شماره موبایل شما تایید نشده است" });
            }

            //Login part:
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Name, user.FullName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties()
            {
                IsPersistent = login.IsRemember,
                AllowRefresh = true
            };

            HttpContext.SignInAsync(principal, properties);

            return Json(new { success = true, message = "ورود شما موفقیت آمیز بود" });
        }

        #endregion

        #region Logout

        [Route("Logout")]
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        #endregion

        #region Forget Password

        [HttpPost]
        public IActionResult SendForgetPasswordSms(string activeCode)
        {
            var user = _userService.GetUserByActiveCode(activeCode);

            if (user != null && user.IsActive == true)
            {
                var tempCode = new TempCode()
                {
                    UserId = user.UserId,
                    Code = NameGenerator.GenerateRandomFourDigitNumber(),
                    SendCodeDateTime = DateTime.Now,
                    Type = (byte)TempCodeType.ForgetPassword
                };
                _userService.AddTempCode(tempCode);

                var message = "آکادمی آموزشی فاطمه کهکشان" +
                              "\n" + user.FullName + "عزیز. " +
                              "\n" + "کد یکبار مصرف تغییر رمز شما: " +
                              tempCode.Code;

                //_smsSevice.SendSms(user.PhoneNumber, message);
                _ = _emailService.SendEmailAsync(user.UserEmail, "کد تایید شماره موبایل", message);

                return Json(new { success = true, message = "کد تایید به شماره موبایل شما ارسال شد" });
            }

            return Json(new { success = false, message = "کد تایید به شماره موبایل شما ارسال نشد" });
        }

        [Route("ForgetPassword")]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public IActionResult ForgetPassword(ForgetPasswordViewModel forget)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "اطلاعات ورودی معتبر نمی باشند" });
            }

            var user = _userService.GetUserByPhoneNumber(forget.PhoneNumber);

            if (user is null)
            {
                return Json(new { success = false, message = "کاربری با شماره موبایل وارد شده یافت نشد" });
            }

            return Json(new { success = true, message = "", activeCode = user.ActiveCode });
        }

        #endregion

        #region Verification Forget Password Code

        [Route("[controller]/[action]/{activeCode}")]
        public IActionResult VerificationForgetPasswordCode(string activeCode)
        {
            var user = _userService.GetUserByActiveCode(activeCode);

            if (user != null)
            {
                return View(user);
            }

            return View("Login");
        }

        [HttpPost]
        public IActionResult VerificationForgetPasswordCode(int userId, string tempCode)
        {
            if (!_userService.ValidateForgetPasswordTempCode(userId, tempCode))
            {
                return Json(new { success = false, message = "کد تایید صحیح نمی باشد یا منقضی شده است" });

            }

            if (!_userService.UpdateActiveCode(userId))
            {
                return Json(new { success = false, message = "در فرآیند تایید شماره موبایل مشکلی بوجود آمده است" });
            }

            var activeCode = _userService.GetActiveCode(userId);

            if (activeCode == null)
            {
                return Json(new { success = false, message = "در فرآیند تایید شماره موبایل مشکلی بوجود آمده است" });
            }

            return Json(new { success = true, message = "شماره موبایل شما تایید شد", activeCode = activeCode });
        }

        #endregion

        #region ResetPassword

        
        public IActionResult ResetPassword(string id)
        {
            return View(new ResetPasswordViewModel()
            {
                ActiveCode = id
            });
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel reset)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "اطلاعات ورودی معتبر نمی باشند" });
            }

            var user = _userService.GetUserByActiveCode(reset.ActiveCode);

            if (user is null)
            {
                return Json(new { success = false, message = "کاربری یافت نشد" });
            }

            var newPassword = PasswordHelper.EncodePasswordSha256(reset.Password);
            user.Password = newPassword;

            if (!_userService.UpdateUser(user))
            {
                return Json(new { success = false, message = "در فرآیند تغییر رمز عبور مشکلی پیش آمده است" });
            }

            return Json(new { success = true, message = "رمز عبور با موفقیت تغییر پیدا کرد" });
        }

        #endregion

    }
}
