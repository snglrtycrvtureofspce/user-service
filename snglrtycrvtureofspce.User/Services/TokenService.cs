using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using snglrtycrvtureofspce.User.Data.Entities;
using snglrtycrvtureofspce.User.Extensions;

namespace snglrtycrvtureofspce.User.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
    public string CreateToken(ApplicationUserEntity user, List<IdentityRole<Guid>> roles)
    {
        var token = user
            .CreateClaims(roles)
            .CreateJwtToken(configuration);
        
        var tokenHandler = new JwtSecurityTokenHandler();
        
        return tokenHandler.WriteToken(token);
    }
}