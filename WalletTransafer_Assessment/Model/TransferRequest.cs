namespace WalletTransafer_Assessment.Model
{
    public class TransferRequest
    {
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
        public decimal Amount { get; set; }
    }
}
