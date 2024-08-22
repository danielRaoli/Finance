namespace Finance.API.Application.Requests
{
    public class DeleteTransactionRequest
    {
        public Guid TransactionId { get; set; }
        public Guid UserId { get; set; }
    }
}
