using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.MediatR.Users.Queries.GetPagedUsers;

public record GetPagedUsersQuery : IRequest<PagedList<UserBriefDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetPagedUsersQueryHandler : IRequestHandler<GetPagedUsersQuery, PagedList<UserBriefDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPagedUsersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedList<UserBriefDto>> Handle(
        GetPagedUsersQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.UserRepository.GetPageListAsync<UserBriefDto>(
            orderBy: x => x.OrderBy(x => x.Id),
            pageNumber: request.PageNumber,
            pageSize: request.PageSize);
        return result;
    }
}
