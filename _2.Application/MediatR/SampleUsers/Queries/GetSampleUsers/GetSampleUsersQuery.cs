using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Common;
using MediatR;

namespace Application.MediatR.SampleUsers.Queries.GetSampleUsers;

[Authorize(Policy = PolicyNames.CanViewAllSampleUsers)]
public record GetSampleUsersQuery : IRequest<IReadOnlyCollection<SampleUserBriefDto>>;

public class GetSampleUsersQueryHandler : IRequestHandler<GetSampleUsersQuery, IReadOnlyCollection<SampleUserBriefDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSampleUsersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyCollection<SampleUserBriefDto>> Handle(GetSampleUsersQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.SampleUserRepository.GetAllAsync<SampleUserBriefDto>(
            orderBy: x => x.OrderBy(x => x.Id),
            cancellationToken: cancellationToken);
        return result;
    }
}
