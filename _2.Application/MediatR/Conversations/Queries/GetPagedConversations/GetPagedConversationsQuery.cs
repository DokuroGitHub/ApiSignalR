using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.MediatR.Conversations.Queries.GetPagedConversations;

public record GetPagedConversationsQuery : IRequest<PagedList<ConversationBriefDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetPagedConversationsQueryHandler : IRequestHandler<GetPagedConversationsQuery, PagedList<ConversationBriefDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPagedConversationsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedList<ConversationBriefDto>> Handle(
        GetPagedConversationsQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.ConversationRepository.GetPageListAsync<ConversationBriefDto>(
            orderBy: x => x.OrderBy(x => x.Id),
            pageNumber: request.PageNumber,
            pageSize: request.PageSize);
        return result;
    }
}
