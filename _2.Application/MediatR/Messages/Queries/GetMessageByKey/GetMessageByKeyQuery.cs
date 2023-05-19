using Application.Common.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Domain.Entities;

namespace Application.MediatR.Messages.Queries.GetMessageByKey;

public record GetMessageByKeyQuery : IRequest<MessageBriefDto>
{
    public int Id { get; init; }
};

public class GetMessageByKeyQueryHandler : IRequestHandler<GetMessageByKeyQuery, MessageBriefDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMessageByKeyQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MessageBriefDto?> Handle(GetMessageByKeyQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.MessageRepository.FindAsync<MessageBriefDto>(
            keyValues: request.Id,
            cancellationToken: cancellationToken);
        if (result is null)
        {
            throw new NotFoundException(nameof(Message), request.Id);
        }
        return result;
    }
}
