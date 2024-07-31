using System.Net;

namespace Finance.API.Exceptions
{
    public class OnValidateException(List<string> errors,string message = null) : TransactionsException(message)
    {
        private List<string> _errors = errors;
        public override List<string> GetErrorMessages()
        {
            return _errors;
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.BadRequest;
        }
    }
}
