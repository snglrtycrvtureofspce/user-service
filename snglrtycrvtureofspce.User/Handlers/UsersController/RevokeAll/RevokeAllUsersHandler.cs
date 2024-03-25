using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using snglrtycrvtureofspce.User.Data.Entities;

namespace snglrtycrvtureofspce.User.Handlers.UsersController.RevokeAll;

public class RevokeAllUsersHandler(UserManager<ApplicationUserEntity> userManager) 
    : IRequestHandler<RevokeAllUsersRequest, Unit>
{
    public async Task<Unit> Handle(RevokeAllUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await userManager.Users.ToListAsync(cancellationToken: cancellationToken);

        foreach (var user in users)
        {
            user.RefreshToken = null;
            await userManager.UpdateAsync(user);
        }

        return Unit.Value;
    }
}