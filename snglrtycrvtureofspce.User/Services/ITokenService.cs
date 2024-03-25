using Microsoft.AspNetCore.Identity;
using snglrtycrvtureofspce.User.Data.Entities;

namespace snglrtycrvtureofspce.User.Services;

public interface ITokenService
{
    string CreateToken(ApplicationUserEntity user, List<IdentityRole<Guid>> role);
}