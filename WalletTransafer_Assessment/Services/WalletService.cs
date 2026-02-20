using Microsoft.EntityFrameworkCore;
using WalletTransafer_Assessment.Data;
using WalletTransafer_Assessment.Model;

namespace WalletTransafer_Assessment.Services
{
    public class WalletService : IWalletService
    {
        private readonly WalletDbContext _context;

        public WalletService(WalletDbContext context)
        {
            _context = context;
        }
        public async Task<TransferResponse> TransferAsync(TransferRequest request)
        {

            if (request.Amount <= 0)
                return new TransferResponse { Success = false, Message = "Invalid Amount" };

            if (request.SenderId == request.ReceiverId)
                return new TransferResponse { Success = false, Message = "Cannot transfer to self" };


            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var sender = await _context.Wallets.FirstOrDefaultAsync(x => x.Id == request.SenderId);

                var receiver = await _context.Wallets.FirstOrDefaultAsync(x => x.Id == request.ReceiverId);

                if (sender == null || receiver == null)
                    return new TransferResponse { Success = false, Message = "Sender or Receiver not found" };

                //cannot exceed wallet balance.

                if (sender.WalletBalance < request.Amount)
                    return new TransferResponse { Success = false, Message = "Insufficient balance" };

                //cannot exceed daily transfer limit.
                if (sender.TransferedToday + request.Amount > sender.DailyTransferLimit)
                    return new TransferResponse { Success = false, Message = "Daily transfer limit exceeded" };



                //Debbitting the sender
                sender.WalletBalance -= request.Amount;
                sender.TransferedToday += request.Amount;

                //credit the receiver
                receiver.WalletBalance += request.Amount;


                var walletTransaction = new WalletTransaction
                {
                    SenderId = sender.Id,
                    ReceiverId = receiver.Id,
                    Amount = request.Amount,
                    Status = "Completed"
                };

                _context.WalletTransactions.Add(walletTransaction);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new TransferResponse
                {
                    Success = true,
                    Message = "Transfer successful"
                };
            }


            catch (DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync();
                return new TransferResponse
                {
                    Success = false,
                    Message = "Concurrency Conflict"
                };

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new TransferResponse
                {
                    Success = false,
                    Message = $"Transfer Failed: {ex.Message}"
                };
            }
        }
    }
}
