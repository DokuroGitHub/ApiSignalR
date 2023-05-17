namespace Application.Services.IServices;

public interface IUserService
{
    Task<string?> GetNullableUsernameByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default);
    Task<string?> GetNullableRoleByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default);
}