using GalaxyLearn.Core.DTOs.Wallet;
using GalaxyLearn.DataLayer.Entities.Wallet;

namespace GalaxyLearn.Core.Services.Interfaces
{
    public interface IWalletService
    {
        int UserAccountBalance(int userId);
        List<WalletViewModel> GetUserWallets(int userId);
        public void ChargeWallet(int userId, int amount, string description, bool isPay = false);
        public void AddWallet(Wallet wallet);
    }
}
