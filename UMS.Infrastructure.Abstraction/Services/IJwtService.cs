using System.IdentityModel.Tokens.Jwt;

namespace UMS.Infrastructure.Abstraction.Services;

public interface IJwtService
{
    public string Generate(string id);
    public JwtSecurityToken Verify(string jwt);
}