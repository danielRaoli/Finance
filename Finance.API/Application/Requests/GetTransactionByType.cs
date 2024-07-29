using Finance.API.Domain.Enums;

namespace Finance.API.Application.Requests
{
    public class GetTransactionByType
    {
        public Guid UserId { get; set; }
        public TransactionType Type { get; set; }
    }
}
