using Finance.API.Application.Requests;
using Finance.API.Application.Responses;

namespace Finance.API.Application.Services
{
    public interface ITransactionService
    {
        Task<Response<TransactionResponse>> AddTransaction(AddTransactionRequest request);
        Task<Response<TransactionResponse?>> UpdateTransaction(UpdateTransactionRequest request);
        Task<Response<List<TransactionResponse>>> GetTransactions(AddTransactionRequest request);
        Task<Response<List<TransactionResponse>>> GetAllByType(GetTransactionByType request);
        Task<Response<List<TransactionResponse>>> GetAllGreaterThan(GetTransactionsGreaterThan request);
        Task<Response<TransactionResponse?>> DeleteTransaction(DeleteTransactionRequest request);
    }
}
