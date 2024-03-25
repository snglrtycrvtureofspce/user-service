using MediatR;

namespace snglrtycrvtureofspce.User.Handlers.UsersController.RevokeUser;

public class RevokeUserRequest : IRequest<Unit>
{
    public string Username { get; set; }
}