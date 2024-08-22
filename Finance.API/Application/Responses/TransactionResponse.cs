using Finance.API.Domain.Entities;
using Finance.API.Domain.Enums;


namespace Finance.API.Application.Responses
{
    public class TransactionResponse()
    {

        public Guid Id { get; set; }
        public DateTime Date { get; set; } 
        public decimal Value { get; set; } 
        public TransactionType Type { get; set; }
        public string Description { get; set; }



        public static TransactionResponse FromEntity(Transaction transaction)
        {
            return new TransactionResponse { Id = transaction.Id ,Date = transaction.Date, Value = transaction.Value, Type = transaction.TransactionType, Description = transaction.Description};
        }
    }
}
