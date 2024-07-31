using Finance.API.Domain.Entities;

namespace Finance.API.Application.Services
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
