using AutoMapper;
using Application.Common.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Domain.Entities;

namespace Application.MediatR.Conversations.Queries.GetConversationByKey;

public record GetConversationByKeyQuery : IRequest<ConversationBriefDto>
{
    public int Id { get; init; }
};

public class GetConversationByKeyQueryHandler : IRequestHandler<GetConversationByKeyQuery, ConversationBriefDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetConversationByKeyQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ConversationBriefDto?> Handle(GetConversationByKeyQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.ConversationRepository.FindAsync<ConversationBriefDto>(
            keyValues: request.Id,
            cancellationToken: cancellationToken);
        if (result is null)
        {
            throw new NotFoundException(nameof(Conversation), request.Id);
        }
        return result;
    }
}
