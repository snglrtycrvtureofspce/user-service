using MediatR;
using snglrtycrvtureofspce.User.Models.Identity;

namespace snglrtycrvtureofspce.User.Handlers.UsersController.AuthenticateUser;

public class AuthenticateUserRequest : IRequest<AuthResponse>
{
    public string Email { get; init; }
    
    public string Password { get; init; }
}