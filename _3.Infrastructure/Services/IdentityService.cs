using Application.Common.Models;
using Application.Common.Interfaces;
using Application.Services.IServices;

namespace Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly IUserService _userService;
    private readonly List<Role> Roles = new(){
        new Role
        {
            Name = "Admin",
            Policies = new List<string> {
                "FullAccess",
            },
        },
        new Role
        {
            Name = "User",
            Policies = new List<string> {
                "CanViewAllUsers"
            },
        }
    };

    public IdentityService(
        IUserService userService)
    {
        _userService = userService;
    }

    public async Task<string?> GetUsernameAsync(int userId)
    => await _userService.GetNullableUsernameByUserIdAsync(userId);

    public async Task<bool> IsInRoleAsync(int userId, string role)
    {
        var userRole = await _userService.GetNullableRoleByUserIdAsync(userId);
        return userRole == role;
    }

    public async Task<bool> AuthorizeAsync(int userId, string policyName)
    {
        if (userId == int.MaxValue)
        {
            return true; // super admin
        }
        var role = await _userService.GetNullableRoleByUserIdAsync(userId);
        var isValid = Roles.Any(x => x.Name == role && x.Policies.Any(x => x == "FullAccess" || x == policyName));
        return isValid;
    }

    public Task<(Result Result, int UserId)> CreateUserAsync(string userName, string password)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteUserAsync(int userId)
    {
        throw new NotImplementedException();
    }
}

public class Role
{
#pragma warning disable 
    public string Name { get; set; }
    public List<string> Policies { get; set; }
}
