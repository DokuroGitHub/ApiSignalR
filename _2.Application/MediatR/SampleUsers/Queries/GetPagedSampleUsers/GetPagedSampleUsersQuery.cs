using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.MediatR.SampleUsers.Queries.GetPagedSampleUsers;

public record GetPagedSampleUsersQuery : IRequest<PagedList<SampleUserBriefDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetPagedSampleUsersQueryHandler : IRequestHandler<GetPagedSampleUsersQuery, PagedList<SampleUserBriefDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPagedSampleUsersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedList<SampleUserBriefDto>> Handle(
        GetPagedSampleUsersQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.SampleUserRepository.GetPageListAsync<SampleUserBriefDto>(
            orderBy: x => x.OrderBy(x => x.Id),
            pageNumber: request.PageNumber,
            pageSize: request.PageSize);
        return result;
    }
}
