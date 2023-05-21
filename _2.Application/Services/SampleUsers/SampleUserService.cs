using Application.Common.Interfaces;
using Application.Services.IServices;

namespace Application.Services.SampleUsers;

partial class SampleUserService : ISampleUserService
{
    private readonly IUnitOfWork _unitOfWork;

    public SampleUserService(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<string?> GetNullableUsernameByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default)
    {
        var result = await _unitOfWork.SampleUserRepository.SingleOrDefaultAsync<GetUsernameByUserIdDto>(
            where: x => x.Id == userId,
            cancellationToken: cancellationToken);
        return result?.Username;
    }

    public async Task<string?> GetNullableRoleByUserIdAsync(
        int userId,
        CancellationToken cancellationToken = default)
    {
        var result = await _unitOfWork.SampleUserRepository.SingleOrDefaultAsync<GetRoleByUserIdDto>(
            where: x => x.Id == userId,
            cancellationToken: cancellationToken);
        return result?.Role;
    }
}
