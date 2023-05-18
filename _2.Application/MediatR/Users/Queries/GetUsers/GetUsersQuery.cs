using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Common;
using MediatR;

namespace Application.MediatR.Users.Queries.GetUsers;

[Authorize(Policy = PolicyNames.CanViewAllUsers)]
public record GetUsersQuery : IRequest<IReadOnlyCollection<UserBriefDto>>;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IReadOnlyCollection<UserBriefDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUsersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyCollection<UserBriefDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.UserRepository.GetAllAsync<UserBriefDto>(
            orderBy: x => x.OrderBy(x => x.Id),
            cancellationToken: cancellationToken);
        return result;
    }
}
