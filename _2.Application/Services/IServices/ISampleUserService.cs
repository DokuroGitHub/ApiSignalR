namespace Application.Services.IServices;

public interface ISampleUserService
{
    Task<string?> GetNullableUsernameByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default);
    Task<string?> GetNullableRoleByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default);
}