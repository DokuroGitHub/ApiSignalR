using Application.Common.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Domain.Entities;

namespace Application.MediatR.Participants.Queries.GetParticipantByKey;

public record GetParticipantByKeyQuery : IRequest<ParticipantBriefDto>
{
    public int ConversationId { get; init; }
    public int UserId { get; init; }
};

public class GetParticipantByKeyQueryHandler : IRequestHandler<GetParticipantByKeyQuery, ParticipantBriefDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetParticipantByKeyQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ParticipantBriefDto?> Handle(GetParticipantByKeyQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.ParticipantRepository.FindAsync<ParticipantBriefDto>(
            cancellationToken: cancellationToken,
            request.ConversationId, request.UserId);
        if (result is null)
        {
            throw new NotFoundException(nameof(Participant), new { request.ConversationId, request.UserId });
        }
        return result;
    }
}
