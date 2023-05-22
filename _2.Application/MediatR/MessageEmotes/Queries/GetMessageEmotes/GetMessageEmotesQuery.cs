using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Common;
using MediatR;

namespace Application.MediatR.MessageEmotes.Queries.GetMessageEmotes;

[Authorize(Policy = PolicyNames.CanViewAllMessageEmotes)]
public record GetMessageEmotesQuery : IRequest<IReadOnlyCollection<MessageEmoteBriefDto>>;

public class GetMessageEmotesQueryHandler : IRequestHandler<GetMessageEmotesQuery, IReadOnlyCollection<MessageEmoteBriefDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMessageEmotesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyCollection<MessageEmoteBriefDto>> Handle(GetMessageEmotesQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.MessageEmoteRepository.GetAllAsync<MessageEmoteBriefDto>(
            orderBy: x => x.OrderBy(x => x.MessageId).ThenBy(x => x.UserId),
            cancellationToken: cancellationToken);
        return result;
    }
}
