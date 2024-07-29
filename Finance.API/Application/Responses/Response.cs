namespace Finance.API.Application.Responses
{
    public class Response<T>(T data,int code = 200, string message = null)
    {
        public  int Code { get; private set; } = code;


        public string Message { get; set; } = message;

        public T Data { get; set; } = data;


    }
}
