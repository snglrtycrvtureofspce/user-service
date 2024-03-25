using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using snglrtycrvtureofspce.User.Data.Entities;

namespace snglrtycrvtureofspce.User.Data;

public sealed class UsersDbContext : IdentityDbContext<ApplicationUserEntity, IdentityRole<Guid>, Guid>
{
    public UsersDbContext (DbContextOptions<UsersDbContext> options)
        : base(options)
    {
        Database.Migrate();
    }
    public UsersDbContext() { }
}