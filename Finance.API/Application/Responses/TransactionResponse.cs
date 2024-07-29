using Finance.API.Domain.Entities;
using Finance.API.Domain.Enums;


namespace Finance.API.Application.Responses
{
    public class TransactionResponse()
    {

        public DateTime Date { get; set; } 
        public decimal Value { get; set; } 
        public TransactionType Type { get; set; }



        public static TransactionResponse FromEntity(Transaction transaction)
        {
            return new TransactionResponse { Date = transaction.Date, Value = transaction.Value, Type = transaction.TransactionType};
        }
    }
}
