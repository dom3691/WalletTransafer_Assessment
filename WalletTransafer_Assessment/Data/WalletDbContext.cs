using Microsoft.EntityFrameworkCore;
using WalletTransafer_Assessment.Model;

namespace WalletTransafer_Assessment.Data
{
    public class WalletDbContext : DbContext
    {
        public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options)
        {
        }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>()
                .Property(w => w.RowVersion)
                .IsRowVersion();
        }
    }
}
