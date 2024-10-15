using System.ComponentModel.DataAnnotations;

namespace GalaxyLearn.Core.DTOs.Wallet
{
    public class ChargeWalletViewModel
    {
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [Range(5000, int.MaxValue, ErrorMessage = "{0} باید حداقل {1} تومان باشد")]
        public int Amount { get; set; }
    }
}
