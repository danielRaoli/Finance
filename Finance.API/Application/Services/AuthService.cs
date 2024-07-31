using Finance.API.Application.Requests;
using Finance.API.Domain.Entities;
using Finance.API.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Finance.API.Application.Services
{
    public class AuthService(UserManager<User> userManager, SignInManager<User> signInManager) : IAuthService
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly SignInManager<User> _signManager = signInManager;
        private readonly ITokenService _tokenService;
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
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
            {
                throw new UnauthorizedAccessException(Resource.NOT_REGISTER);

            }

            var result = await _signManager.PasswordSignInAsync(request.UserName, request.Password, false, false);

            return result.Succeeded ? _tokenService.GenerateToken(user) : throw new UnauthorizedAccessException(Resource.NOT_REGISTER); ;

        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }
    }
}
