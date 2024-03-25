using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using snglrtycrvtureofspce.User.Data;
using snglrtycrvtureofspce.User.Data.Entities;
using snglrtycrvtureofspce.User.Handlers.UsersController.AuthenticateUser;
using snglrtycrvtureofspce.User.Handlers.UsersController.RefreshToken;
using snglrtycrvtureofspce.User.Handlers.UsersController.RegisterUser;
using snglrtycrvtureofspce.User.Handlers.UsersController.RevokeAll;
using snglrtycrvtureofspce.User.Handlers.UsersController.RevokeUser;
using snglrtycrvtureofspce.User.Models.Identity;

namespace snglrtycrvtureofspce.User.Controllers;

[ApiController]
[Route("users")]
[Produces("application/json")]
public class UsersController(ISender sender, UsersDbContext context, UserManager<ApplicationUserEntity> userManager) : 
    ControllerBase
{
    /// <summary>
    /// The method provider possibility to authenticate a user by email and password
    /// </summary>
    /// <returns></returns>
    [HttpPost("AuthenticateUser", Name = "AuthenticateUser")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult> AuthenticateUser([FromBody] AuthenticateUserRequest request) => 
        Ok(await sender.Send(request));
    
    /// <summary>
    /// The method provider possibility to register a user
    /// </summary>
    /// <returns></returns>
    [HttpPost("RegisterUser", Name = "RegisterUser")]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterUserRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(request);
        
        if (request.Password != request.PasswordConfirm)
        {
            ModelState.AddModelError("PasswordConfirm", "The passwords don't match");
            return BadRequest(ModelState);
        }
        
        var user = new ApplicationUserEntity
        {
            UserName = request.UserName,
            Email = request.Email, 
            FirstName = request.FirstName, 
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            DateOfBirth = request.DateOfBirth,
            Country = request.Country,
            City = request.City,
            Agreement = request.Agreement
        };
        
        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        
            return BadRequest(ModelState);
        }
        
        var findUser = await context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

        if (findUser == null)
        {
            throw new Exception($"User {request.Email} not found");
        }

        await userManager.AddToRoleAsync(findUser, RoleConsts.Member);
            
        return await AuthenticateUser(new AuthenticateUserRequest
        {
            Email = request.Email,
            Password = request.Password
        });
    }
    
    /// <summary>
    /// The method provider possibility to refresh a token by access token and refresh token
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("refresh-token", Name = "RefreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request) => 
        Ok(await sender.Send(request));

    /// <summary>
    /// The method provider possibility to revoke a user by bearer token
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("revoke", Name = "RevokeUser")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> RevokeUser([FromBody] RevokeUserRequest request) => Ok(await sender.Send(request));
    
    /// <summary>
    /// The method provider possibility to revoke all by bearer token
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("revoke-all", Name = "RevokeAllUsers")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> RevokeAllUsers() => Ok(await sender.Send(new RevokeAllUsersRequest()));
}