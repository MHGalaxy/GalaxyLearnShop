using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GalaxyLearn.DataLayer.Entities.Wallet
{
    public class WalletType
    {
        public WalletType()
        {
            
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WalletTypeId { get; set; }

        [Required]
        [MaxLength(150)]
        public string WalletTypeName { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string WalletTypeTitle { get; set; } = string.Empty;


        #region Relations

        public virtual List<Wallet> Wallets { get; set; }

        #endregion

    }
}
