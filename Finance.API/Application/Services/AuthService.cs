using Finance.API.Application.Requests;
using Finance.API.Domain.Entities;
using Finance.API.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Finance.API.Application.Services
{
    public class AuthService(UserManager<User> userManager, ITokenService tokenService) : IAuthService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly ITokenService _tokenService = tokenService;
        public async Task CreateAccount(CreateAccountRequest request)
        {
            var user = new User { UserName = request.UserName, Email = request.Email };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded is false)
            {
                throw new Exception("internal Error");

            }
        }

        public async Task<string> Login(LoginModelRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName) ?? throw new UnauthorizeException(Resource.NOT_REGISTER);
            ;


            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);

           
            return passwordValid ? _tokenService.GenerateToken(user) : throw new UnauthorizeException(Resource.NOT_REGISTER); ;

        }

    }
}
