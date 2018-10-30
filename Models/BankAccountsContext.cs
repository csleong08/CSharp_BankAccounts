using Microsoft.EntityFrameworkCore;

namespace BankAccounts.Models
{
    public class BankAccountsContext : DbContext
    {
        public DbSet<Users> users { get; set; } // always make users lowercase
        public DbSet<Transactions> transactions { get; set; } // always make users lowercase

        // base() calls the parent class' constructor passing the "options" parameter along
        public BankAccountsContext(DbContextOptions<BankAccountsContext> options) : base(options) { }
    }
}