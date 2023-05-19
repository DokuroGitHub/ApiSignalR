using Application.Common.Interfaces;
using MediatR;

namespace Application.MediatR.Auth.Queries.GetCurrentUserId;

public record GetCurrentUserIdQuery : IRequest<int>;

public class GetCurrentUserIdQueryHandler : IRequestHandler<GetCurrentUserIdQuery, int>
{
    private readonly ICurrentUserService _currentUserService;

    public GetCurrentUserIdQueryHandler(
        ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public async Task<int> Handle(GetCurrentUserIdQuery request, CancellationToken cancellationToken)
    {
        var getUserIdTask = Task.Run(() => _currentUserService.UserId);
        return (await getUserIdTask) ?? 0;
    }
}
