using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.MediatR.MessageEmotes.Queries.GetPagedMessageEmotes;

public record GetPagedMessageEmotesQuery : IRequest<PagedList<MessageEmoteBriefDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetPagedMessageEmotesQueryHandler : IRequestHandler<GetPagedMessageEmotesQuery, PagedList<MessageEmoteBriefDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPagedMessageEmotesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedList<MessageEmoteBriefDto>> Handle(
        GetPagedMessageEmotesQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.MessageEmoteRepository.GetPageListAsync<MessageEmoteBriefDto>(
            orderBy: x => x.OrderBy(x => x.MessageId).ThenBy(x => x.UserId),
            pageNumber: request.PageNumber,
            pageSize: request.PageSize);
        return result;
    }
}
