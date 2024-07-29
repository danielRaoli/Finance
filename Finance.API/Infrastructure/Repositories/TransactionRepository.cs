using Finance.API.Application.Requests;
using Finance.API.Domain.Entities;
using Finance.API.Domain.Repositories;
using Finance.API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Finance.API.Infrastructure.Repositories
{
    public class TransactionRepository(AppDbContext context) : ITransactionRepository
    {
        private AppDbContext _context = context;
        public async Task<Transaction> AddTransaction(Transaction transaction)
        {
             _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }
        public async Task<List<Transaction>> GetAllByUser(Guid userId)
        {
            return await _context.Transactions.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<List<Transaction>> GetAllByType(GetTransactionByType request)
        {
            return await _context.Transactions.Where(t => t.UserId == request.UserId && t.TransactionType == request.Type).ToListAsync();
        }



        public async Task<List<Transaction>> GetAllGreaterThan(GetTransactionsGreaterThan request)
        {
            return await _context.Transactions.Where(t => t.UserId ==  request.UserId && Math.Abs(t.Value) >= request.Value).ToListAsync();
        }
    }
}
