using Finance.API.Domain.Entities;
using Finance.API.Domain.Enums;

namespace Finance.API.Application.Requests
{
    public class AddTransactionRequest
    {
        public Guid UserId { get; set; }
        public decimal Value { get; set; }
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }

        public Transaction ToEntity() => new(this.UserId, this.Value, this.Type,this.Date);
    }
}
