using WalletTransafer_Assessment.Model;

namespace WalletTransafer_Assessment.Services
{
    public interface IWalletService
    {
        Task<TransferResponse> TransferAsync(TransferRequest request);
    }
}
