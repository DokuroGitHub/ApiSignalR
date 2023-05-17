using Application.Common.Interfaces;
using Application.Services.IServices;

namespace Application.Services.Users;

partial class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserService(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<string?> GetNullableUsernameByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default)
    {
        var result = await _unitOfWork.UserRepository.SingleOrDefaultAsync<GetUsernameByUserIdDto>(
            where: x => x.Id == userId,
            cancellationToken: cancellationToken);
        return result?.Username;
    }

    public async Task<string?> GetNullableRoleByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default)
    {
        var result = await _unitOfWork.UserRepository.SingleOrDefaultAsync<GetRoleByUserIdDto>(
            where: x => x.Id == userId,
            cancellationToken: cancellationToken);
        return result?.Role;
    }
}
