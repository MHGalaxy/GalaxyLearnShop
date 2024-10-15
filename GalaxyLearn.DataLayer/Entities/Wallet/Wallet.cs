using GalaxyLearn.DataLayer.Entities.User;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GalaxyLearn.DataLayer.Entities.Wallet
{
    public class Wallet
    {
        public Wallet()
        {
            
        }

        [Key]
        public int WalletId { get; set; }

        [DisplayName("نوع تراکنش")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب نمایید")]
        public int WalletTypeId { get; set; }

        [DisplayName("کاربر")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب نمایید")]
        public int UserId { get; set; }

        [DisplayName("مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public int Amount { get; set; }

        [DisplayName("تایید شده")]
        public bool IsPay { get; set; }

        [DisplayName("توضیحات تراکنش")]
        [MaxLength(100, ErrorMessage = "{0} نباید از {1} کاراکتر بیشتر باشد")]
        public string Description { get; set; } = string.Empty;

        [DisplayName("تاریخ و ساعت تراکنش")]
        public DateTime CreateDate { get; set; }



        #region Relations

        public virtual User.User User { get; set; }
        public virtual WalletType WalletType { get; set; }

        #endregion

    }
}
