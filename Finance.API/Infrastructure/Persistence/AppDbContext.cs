using Finance.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Finance.API.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> opts) : DbContext(opts)
    {
        public DbSet<Transaction> Transactions{ get; set; }
    }
}
