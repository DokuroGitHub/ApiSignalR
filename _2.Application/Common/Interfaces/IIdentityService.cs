using Application.Common.Models;

namespace Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUsernameAsync(int userId);

    Task<bool> IsInRoleAsync(int userId, string role);

    Task<bool> AuthorizeAsync(int userId, string policyName);

    Task<(Result Result, int UserId)> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(int userId);
}
