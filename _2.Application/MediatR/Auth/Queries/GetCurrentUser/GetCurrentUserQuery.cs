using Application.Common.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Domain.Entities;

namespace Application.MediatR.Auth.Queries.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<UserBriefDto>
{
#pragma warning disable
    public string Username { get; init; }
    public string Password { get; init; }
    public string? PasswordConfirm { get; init; }
};

public class LoginQueryHandler : IRequestHandler<GetCurrentUserQuery, UserBriefDto>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public LoginQueryHandler(
        ICurrentUserService currentUserService,
        IUnitOfWork unitOfWork)
    {
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserBriefDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        if (userId is null)
        {
            throw new NotFoundException(nameof(User), userId);
        }
        var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync<UserBriefDto>(
            where: x => x.Id == userId,
            cancellationToken: cancellationToken);
        if (user is null)
        {
            throw new NotFoundException(nameof(User), userId);
        };
        return user;
    }
}
