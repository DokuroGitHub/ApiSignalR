using Application.Common.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Domain.Entities;

namespace Application.MediatR.MessageEmotes.Queries.GetMessageEmoteByKey;

public record GetMessageEmoteByKeyQuery : IRequest<MessageEmoteBriefDto>
{
    public int MessageId { get; init; }
    public int UserId { get; init; }
};

public class GetMessageEmoteByKeyQueryHandler : IRequestHandler<GetMessageEmoteByKeyQuery, MessageEmoteBriefDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMessageEmoteByKeyQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MessageEmoteBriefDto> Handle(GetMessageEmoteByKeyQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.MessageEmoteRepository.FindAsync<MessageEmoteBriefDto>(
            keyValues: new object[] { request.MessageId, request.UserId },
            cancellationToken);
        if (result is null)
        {
            throw new NotFoundException(nameof(MessageEmote), new { request.MessageId, request.UserId });
        }
        return result;
    }
}
