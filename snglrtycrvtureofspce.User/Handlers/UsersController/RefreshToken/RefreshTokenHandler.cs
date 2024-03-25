using System.IdentityModel.Tokens.Jwt;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using snglrtycrvtureofspce.User.Data.Entities;
using snglrtycrvtureofspce.User.Extensions;

namespace snglrtycrvtureofspce.User.Handlers.UsersController.RefreshToken;

public class RefreshTokenHandler(UserManager<ApplicationUserEntity> userManager, IConfiguration configuration)
    : IRequestHandler<RefreshTokenRequest, IActionResult>
{
    public async Task<IActionResult> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var accessToken = request.AccessToken;
        
        if (accessToken is null)
        {
            return new BadRequestObjectResult("Invalid access token");
        }
        
        var refreshToken = request.RefreshToken;
        
        if (refreshToken is null)
        {
            return new BadRequestObjectResult("Invalid access token");
        }

        var principal = configuration.GetPrincipalFromExpiredToken(accessToken);

        if (principal == null)
        {
            return new BadRequestObjectResult("Invalid access token or refresh token");
        }

        var username = principal.Identity!.Name;
        var user = await userManager.FindByNameAsync(username!);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return new BadRequestObjectResult("Invalid access token or refresh token");
        }

        var newAccessToken = configuration.CreateToken(principal.Claims.ToList());
        var newRefreshToken = configuration.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await userManager.UpdateAsync(user);

        return new ObjectResult(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken
        });
    }
}