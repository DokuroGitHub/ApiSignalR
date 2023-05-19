using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Common;
using MediatR;

namespace Application.MediatR.Messages.Queries.GetMessages;

[Authorize(Policy = PolicyNames.CanViewAllMessages)]
public record GetMessagesQuery : IRequest<IReadOnlyCollection<MessageBriefDto>>;

public class GetMessagesQueryHandler : IRequestHandler<GetMessagesQuery, IReadOnlyCollection<MessageBriefDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMessagesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyCollection<MessageBriefDto>> Handle(GetMessagesQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.MessageRepository.GetAllAsync<MessageBriefDto>(
            orderBy: x => x.OrderBy(x => x.Id),
            cancellationToken: cancellationToken);
        return result;
    }
}
