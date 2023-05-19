using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.MediatR.Messages.Queries.GetPagedMessages;

public record GetPagedMessagesQuery : IRequest<PagedList<MessageBriefDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetPagedMessagesQueryHandler : IRequestHandler<GetPagedMessagesQuery, PagedList<MessageBriefDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPagedMessagesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedList<MessageBriefDto>> Handle(
        GetPagedMessagesQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.MessageRepository.GetPageListAsync<MessageBriefDto>(
            orderBy: x => x.OrderBy(x => x.Id),
            pageNumber: request.PageNumber,
            pageSize: request.PageSize);
        return result;
    }
}
