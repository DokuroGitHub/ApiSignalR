using Application.Common.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Domain.Entities;

namespace Application.MediatR.MessageAttachments.Queries.GetMessageAttachmentByKey;

public record GetMessageAttachmentByKeyQuery : IRequest<MessageAttachmentBriefDto>
{
    public int Id { get; init; }
};

public class GetMessageAttachmentByKeyQueryHandler : IRequestHandler<GetMessageAttachmentByKeyQuery, MessageAttachmentBriefDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMessageAttachmentByKeyQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<MessageAttachmentBriefDto> Handle(GetMessageAttachmentByKeyQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.MessageAttachmentRepository.FindAsync<MessageAttachmentBriefDto>(
            keyValues: request.Id,
            cancellationToken: cancellationToken);
        if (result is null)
        {
            throw new NotFoundException(nameof(MessageAttachment), request.Id);
        }
        return result;
    }
}
