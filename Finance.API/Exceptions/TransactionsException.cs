using System.Net;

namespace Finance.API.Exceptions
{
    public abstract class TransactionsException(string message) : SystemException(message)
    {

        public abstract List<string> GetErrorMessages();
        public abstract HttpStatusCode GetStatusCode();
    }
}
