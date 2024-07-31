using System.Net;

namespace Finance.API.Exceptions
{
    public class NotFoundException(string message) : TransactionsException(message)
    {
        public override List<string> GetErrorMessages()
        {
            return [Message];
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.NotFound;
        }
    }
}
