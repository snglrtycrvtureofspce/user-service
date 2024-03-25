using MediatR;
using Microsoft.AspNetCore.Identity;
using snglrtycrvtureofspce.User.Data.Entities;

namespace snglrtycrvtureofspce.User.Handlers.UsersController.RevokeUser;

public class RevokeUserHandler(UserManager<ApplicationUserEntity> userManager) : IRequestHandler<RevokeUserRequest, 
    Unit>
{
    public async Task<Unit> Handle(RevokeUserRequest request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.Username);
        if (user == null)
        {
            throw new ArgumentException("Invalid user name");
        }

        user.RefreshToken = null;
        await userManager.UpdateAsync(user);

        return Unit.Value;
    }
}