using Application.Common.Interfaces;
using Application.Common.Security;
using Domain.Common;
using MediatR;

namespace Application.MediatR.MessageAttachments.Queries.GetMessageAttachments;

[Authorize(Policy = PolicyNames.CanViewAllMessageAttachments)]
public record GetMessageAttachmentsQuery : IRequest<IReadOnlyCollection<MessageAttachmentBriefDto>>;

public class GetMessageAttachmentsQueryHandler : IRequestHandler<GetMessageAttachmentsQuery, IReadOnlyCollection<MessageAttachmentBriefDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetMessageAttachmentsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyCollection<MessageAttachmentBriefDto>> Handle(GetMessageAttachmentsQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.MessageAttachmentRepository.GetAllAsync<MessageAttachmentBriefDto>(
            orderBy: x => x.OrderBy(x => x.Id),
            cancellationToken: cancellationToken);
        return result;
    }
}
