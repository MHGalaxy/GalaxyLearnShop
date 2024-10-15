using GalaxyLearn.Core.DTOs.Wallet;
using GalaxyLearn.Core.Enums;
using GalaxyLearn.Core.Extensions;
using GalaxyLearn.Core.Services.Interfaces;
using GalaxyLearn.DataLayer.Context;
using GalaxyLearn.DataLayer.Entities.Wallet;
using Microsoft.EntityFrameworkCore;

namespace GalaxyLearn.Core.Services
{
    public class WalletService : IWalletService
    {
        private readonly GalaxyContext _context;
        //private readonly IMapper _mapper;

        public WalletService(GalaxyContext context)
        {
            _context = context;
        }

        public int UserAccountBalance(int userId)
        {
            var depositsList = _context.Wallets //واریزی ها
                .Where(w => w.UserId == userId && w.WalletTypeId == (int)Enums.WalletType.Deposit && w.IsPay)
                .Select(w => w.Amount)
                .ToList();

            var debitsList = _context.Wallets //برداشت ها
                .Where(w => w.UserId == userId && w.WalletTypeId == (int)Enums.WalletType.Debit && w.IsPay)
                .Select(w => w.Amount)
                .ToList();

            var deposits = depositsList != null ? depositsList.Sum() : 0;
            var debits = debitsList != null ? debitsList.Sum() : 0;

            return deposits - debits;
        }

        public List<WalletViewModel> GetUserWallets(int userId)
        {
            return _context.Wallets
                .Include(w => w.WalletType)
                .Where(w => w.UserId == userId && w.IsPay)
                .OrderByDescending(w => w.CreateDate)
                .Take(10)
                .Select(w => new WalletViewModel
                {
                    Amount = w.Amount,
                    WalltType = w.WalletType.WalletTypeTitle,
                    Description = w.Description,
                    Date = w.CreateDate.ToPersianDate(),
                    Time = w.CreateDate.GetTime()
                })
                .ToList();
        }

        public void ChargeWallet(int userId, int amount, string description, bool isPay = false)
        {
            Wallet wallet = new Wallet()
            {
                Amount = amount,
                CreateDate = DateTime.Now,
                Description = description,
                IsPay = isPay,
                WalletTypeId = (int)Enums.WalletType.Deposit,
                UserId = userId
            };
            AddWallet(wallet);
        }

        public void AddWallet(Wallet wallet)
        {
            _context.Wallets.Add(wallet);
            _context.SaveChanges();
        }

    }
}
