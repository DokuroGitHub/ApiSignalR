using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Common;
using MediatR;

namespace Application.MediatR.Conversations.Queries.GetConversations;

[Authorize(Policy = PolicyNames.CanViewAllConversations)]
public record GetConversationsQuery : IRequest<IReadOnlyCollection<ConversationBriefDto>>;

public class GetConversationsQueryHandler : IRequestHandler<GetConversationsQuery, IReadOnlyCollection<ConversationBriefDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetConversationsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyCollection<ConversationBriefDto>> Handle(GetConversationsQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.ConversationRepository.GetAllAsync<ConversationBriefDto>(
            orderBy: x => x.OrderBy(x => x.Id),
            cancellationToken: cancellationToken);
        return result;
    }
}
