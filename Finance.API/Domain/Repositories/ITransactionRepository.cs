using Finance.API.Application.Requests;
using Finance.API.Domain.Entities;

namespace Finance.API.Domain.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> AddTransaction(Transaction transaction);
        Task<bool> UpdateTransaction(UpdateTransactionRequest request);
        Task<List<Transaction>> GetAllByUser(Guid userId);
        Task<List<Transaction>> GetAllByType(GetTransactionByType request);
        Task<List<Transaction>> GetAllGreaterThan(GetTransactionsGreaterThan request);
        Task<bool> DeleteTransaction(DeleteTransactionRequest request);
    }
}
