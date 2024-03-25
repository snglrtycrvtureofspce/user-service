using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using snglrtycrvtureofspce.User.Data;
using snglrtycrvtureofspce.User.Data.Entities;
using snglrtycrvtureofspce.User.Extensions;
using snglrtycrvtureofspce.User.Models.Identity;
using snglrtycrvtureofspce.User.Services;

namespace snglrtycrvtureofspce.User.Handlers.UsersController.AuthenticateUser;

public class AuthenticateUserHandler(UserManager<ApplicationUserEntity> userManager, UsersDbContext context, 
    ITokenService tokenService, IConfiguration configuration) : IRequestHandler<AuthenticateUserRequest, AuthResponse>
{
    public async Task<AuthResponse> Handle(AuthenticateUserRequest request, CancellationToken cancellationToken)
    {
        var managedUser = await userManager.FindByEmailAsync(request.Email);

        if (managedUser == null)
        {
            throw new Exception("Bad credentials");
        }

        var isPasswordValid = await userManager.CheckPasswordAsync(managedUser, request.Password);

        if (!isPasswordValid)
        {
            throw new Exception("Bad credentials");
        }

        var user = context.Users.FirstOrDefault(u => u.Email == request.Email);

        if (user is null)
        {
            throw new UnauthorizedAccessException();
        }

        var roleIds = await context.UserRoles.Where(r => r.UserId == user.Id)
            .Select(x => x.RoleId).ToListAsync(cancellationToken: cancellationToken);
        var roles = context.Roles.Where(x => roleIds.Contains(x.Id)).ToList();

        var accessToken = tokenService.CreateToken(user, roles);
        user.RefreshToken = configuration.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(configuration
            .GetSection("Jwt:RefreshTokenValidityInDays").Get<int>());

        await context.SaveChangesAsync(cancellationToken);

        return new AuthResponse
        {
            Username = user.UserName!,
            Email = user.Email!,
            Token = accessToken,
            RefreshToken = user.RefreshToken
        };
    }
}