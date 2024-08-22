using Finance.API.Domain.Enums;
using System.Text.Json.Serialization;
using System.Transactions;

namespace Finance.API.Application.Requests
{
    public class UpdateTransactionRequest
    {
        [JsonIgnore]
        public Guid TransactionId { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public TransactionType Type { get; set; }
    }
}
