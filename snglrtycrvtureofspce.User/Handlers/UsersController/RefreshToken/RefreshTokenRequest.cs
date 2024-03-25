using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace snglrtycrvtureofspce.User.Handlers.UsersController.RefreshToken;

public class RefreshTokenRequest : IRequest<IActionResult>
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}