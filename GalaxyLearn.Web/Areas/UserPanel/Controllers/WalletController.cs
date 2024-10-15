using GalaxyLearn.Core.DTOs.Wallet;
using GalaxyLearn.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GalaxyLearn.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class WalletController : Controller
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [Route("UserPanel/Wallet")]
        public IActionResult Index()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            ViewBag.UserWallets = _walletService.GetUserWallets(userId);
            ViewBag.UserAccountBalance = _walletService.UserAccountBalance(userId);
            return View();
        }

        [Route("UserPanel/Wallet")]
        [HttpPost]
        public IActionResult Index(ChargeWalletViewModel charge)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

            if(!ModelState.IsValid)
            {
                return Json(new { success = false, message = "اطلاعات ورودی معتبر نمی باشد" });
            }

            #region Online Payment 

            var payment = new ZarinpalSandbox.Payment(charge.Amount);
            //var res = payment.PaymentRequest("شارژ کیف پول", );

            #endregion

            _walletService.ChargeWallet(userId, charge.Amount, "شارژ حساب");
            return Json(new { success = true, message = "حساب شما با موفقیت شارژ شد" });
        }
    }
}
