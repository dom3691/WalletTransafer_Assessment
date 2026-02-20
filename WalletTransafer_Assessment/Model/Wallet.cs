using System.ComponentModel.DataAnnotations;

namespace WalletTransafer_Assessment.Model
{
    public class Wallet
    {
        public long Id { get; set; }
        public decimal WalletBalance { get; set; }
        public decimal DailyTransferLimit { get; set; }
        public decimal TransferedToday { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
