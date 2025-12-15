using FindMyPark.API.Data;
using FindMyPark.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindMyPark.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WalletController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<WalletDto>> GetWallet(int userId)
        {
            var wallet = await _context.Wallets
                .FirstOrDefaultAsync(w => w.UserId == userId);

            if (wallet == null)
            {
                // Auto-create wallet if it doesn't exist for the user
                wallet = new Wallet { UserId = userId, Balance = 0 };
                _context.Wallets.Add(wallet);
                await _context.SaveChangesAsync();
            }

            var transactions = await _context.Transactions
                .Where(t => t.WalletId == wallet.Id)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            return new WalletDto
            {
                Id = wallet.Id,
                Balance = wallet.Balance,
                Transactions = transactions
            };
        }

        [HttpPost("add-funds")]
        public async Task<IActionResult> AddFunds(AddFundsDto dto)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == dto.UserId);
            if (wallet == null)
            {
                wallet = new Wallet { UserId = dto.UserId, Balance = 0 };
                _context.Wallets.Add(wallet);
            }

            // Mock Payment Gateway Logic
            wallet.Balance += dto.Amount;
            
            var transaction = new Transaction
            {
                WalletId = wallet.Id,
                Amount = dto.Amount,
                Type = "Credit",
                Description = "Added Funds (Mock Bank)" // In real world, this comes from Gateway webhook
            };
            
            var notification = new Notification
            {
                UserId = dto.UserId,
                Title = "Funds Added",
                Message = $"₹{dto.Amount} added to your wallet.",
                IsRead = false
            };
            
            _context.Wallets.Update(wallet);
            _context.Transactions.Add(transaction);
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Funds added successfully", NewBalance = wallet.Balance });
        }
    }

    public class WalletDto
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public List<Transaction> Transactions { get; set; }
    }

    public class AddFundsDto
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
    }
}
