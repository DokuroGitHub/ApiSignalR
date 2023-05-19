using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.MediatR.MessageAttachments.Queries.GetPagedMessageAttachments;

public record GetPagedMessageAttachmentsQuery : IRequest<PagedList<MessageAttachmentBriefDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetPagedMessageAttachmentsQueryHandler : IRequestHandler<GetPagedMessageAttachmentsQuery, PagedList<MessageAttachmentBriefDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPagedMessageAttachmentsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedList<MessageAttachmentBriefDto>> Handle(
        GetPagedMessageAttachmentsQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.MessageAttachmentRepository.GetPageListAsync<MessageAttachmentBriefDto>(
            orderBy: x => x.OrderBy(x => x.Id),
            pageNumber: request.PageNumber,
            pageSize: request.PageSize);
        return result;
    }
}
