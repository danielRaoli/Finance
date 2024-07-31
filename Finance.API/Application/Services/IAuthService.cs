using Finance.API.Application.Requests;

namespace Finance.API.Application.Services
{
    public interface IAuthService
    {
        Task<string> Login(LoginModelRequest request);

        Task CreateAccount(CreateAccountRequest request);

        Task Logout();
    }
}
