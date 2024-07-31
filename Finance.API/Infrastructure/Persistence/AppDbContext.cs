using Finance.API.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Finance.API.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions opts) : IdentityDbContext<User,IdentityRole<Guid>,Guid>(opts)
    {


        public DbSet<Transaction> Transactions { get; set; }
    }
}
