using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.MediatR.Participants.Queries.GetPagedParticipants;

public record GetPagedParticipantsQuery : IRequest<PagedList<ParticipantBriefDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetPagedParticipantsQueryHandler : IRequestHandler<GetPagedParticipantsQuery, PagedList<ParticipantBriefDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPagedParticipantsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedList<ParticipantBriefDto>> Handle(
        GetPagedParticipantsQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.ParticipantRepository.GetPageListAsync<ParticipantBriefDto>(
            orderBy: x => x.OrderBy(x => x.ConversationId).ThenBy(x => x.CreatedAt),
            pageNumber: request.PageNumber,
            pageSize: request.PageSize);
        return result;
    }
}
