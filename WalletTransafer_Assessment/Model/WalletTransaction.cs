namespace WalletTransafer_Assessment.Model
{
    public class WalletTransaction
    {
        public Guid TrabsactionId { get; set; } = Guid.NewGuid();
        public long SenderId { get; set; }
        public long ReceiverId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
