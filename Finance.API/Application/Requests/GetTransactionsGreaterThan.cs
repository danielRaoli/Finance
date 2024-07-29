namespace Finance.API.Application.Requests
{
    public class GetTransactionsGreaterThan
    {
        public Guid UserId { get; set; }
        public decimal Value { get; set; }
    }
}
