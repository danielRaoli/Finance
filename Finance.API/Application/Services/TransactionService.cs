using Finance.API.Application.Requests;
using Finance.API.Application.Responses;
using Finance.API.Domain.Repositories;

namespace Finance.API.Application.Services
{
    public class TransactionService(ITransactionRepository transactionRepository) : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository = transactionRepository;

        public async Task<Response<TransactionResponse>> AddTransaction(AddTransactionRequest request)
        {
            var entity = request.ToEntity();
            var createdResponse = await _transactionRepository.AddTransaction(entity);

            return new Response<TransactionResponse>(TransactionResponse.FromEntity(createdResponse), 201, "transaction register with success");

        }

        public async Task<Response<TransactionResponse?>> DeleteTransaction(DeleteTransactionRequest request)
        {
            var result = await _transactionRepository.DeleteTransaction(request);

            return result ? new Response<TransactionResponse?>(null, 204, "Transaction Remove with success") : new Response<TransactionResponse?>(null, 401, "transaction could not be removed, try again");
        }

        public async Task<Response<List<TransactionResponse>>> GetAllByType(GetTransactionByType request)
        {
            var transactions = await _transactionRepository.GetAllByType(request);
            var responseTransactions = transactions.Select(t => TransactionResponse.FromEntity(t)).ToList();

            return new Response<List<TransactionResponse>>(responseTransactions, 200);

        }

        public async Task<Response<List<TransactionResponse>>> GetAllGreaterThan(GetTransactionsGreaterThan request)
        {
            var transactions = await _transactionRepository.GetAllGreaterThan(request);
            var responseTransactions = transactions.Select(t => TransactionResponse.FromEntity(t)).ToList();

            return new Response<List<TransactionResponse>>(responseTransactions, 200);
        }

        public async Task<Response<List<TransactionResponse>>> GetTransactions(AddTransactionRequest request)
        {
            var transactionsDb = await _transactionRepository.GetAllByUser(request.UserId);
            var listResponseTransactions = transactionsDb.Select(t => TransactionResponse.FromEntity(t)).ToList();

            return new Response<List<TransactionResponse>>(listResponseTransactions, 200);
        }

        public async Task<Response<TransactionResponse?>> UpdateTransaction(UpdateTransactionRequest request)
        {
            var result = await _transactionRepository.UpdateTransaction(request);

            return result is true ? new Response<TransactionResponse?>(null, 204, "Transaction updated with succes") : new Response<TransactionResponse?>(null, 401, "transaction could not be updated, try again ");
        }
    }
}
