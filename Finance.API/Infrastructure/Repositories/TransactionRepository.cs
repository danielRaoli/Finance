using Finance.API.Application.Requests;
using Finance.API.Domain.Entities;
using Finance.API.Domain.Repositories;
using Finance.API.Exceptions;
using Finance.API.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
            return await _context.Transactions.Where(t => t.UserId == request.UserId && Math.Abs(t.Value) >= request.Value).ToListAsync();
        }

        public async Task<bool> UpdateTransaction(UpdateTransactionRequest request)
        {
            var entity = await _context.Transactions.FirstOrDefaultAsync(t => t.UserId == request.UserId && t.Id == request.TransactionId);

            if (entity == null)
            {

                throw new NotFoundException(Resource.NOT_FOUND_ERROR);

            }

            entity.Value = request.Value;
            entity.TransactionType = request.Type;
            entity.Description = request.Description;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTransaction(DeleteTransactionRequest request)
        {
            var entity = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == request.TransactionId && t.UserId == request.UserId) ??
                throw new NotFoundException(Resource.NOT_FOUND_ERROR); ;

            _context.Transactions.Remove(entity);
            return await _context.SaveChangesAsync() > 0;

        }
    }
}
