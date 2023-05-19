using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Common;
using MediatR;

namespace Application.MediatR.Participants.Queries.GetParticipants;

[Authorize(Policy = PolicyNames.CanViewAllParticipants)]
public record GetParticipantsQuery : IRequest<IReadOnlyCollection<ParticipantBriefDto>>;

public class GetParticipantsQueryHandler : IRequestHandler<GetParticipantsQuery, IReadOnlyCollection<ParticipantBriefDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetParticipantsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyCollection<ParticipantBriefDto>> Handle(GetParticipantsQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.ParticipantRepository.GetAllAsync<ParticipantBriefDto>(
            orderBy: x => x.OrderBy(x => x.ConversationId).ThenBy(x => x.CreatedAt),
            cancellationToken: cancellationToken);
        return result;
    }
}
