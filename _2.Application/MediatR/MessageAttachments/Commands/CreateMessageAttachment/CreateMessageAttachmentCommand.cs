using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using MediatR;

namespace Application.MediatR.MessageAttachments.Commands.CreateMessageAttachment;

#pragma warning disable
public record CreateMessageAttachmentCommand : IRequest<int>, IMapFrom<MessageAttachment>
{
    public int MessageId { get; init; }
    public string? FileUrl { get; init; }
    public string? ThumbUrl { get; init; }
    public AttachmentType? Type { get; init; }
    //* ref
    // map
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateMessageAttachmentCommand, MessageAttachment>();
    }
}
#pragma warning restore

public class CreateMessageAttachmentCommandHandler : IRequestHandler<CreateMessageAttachmentCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateMessageAttachmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateMessageAttachmentCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<MessageAttachment>(request);
        entity.AddDomainEvent(new MessageAttachmentBeforeInsertEvent(entity));
        _unitOfWork.MessageAttachmentRepository.Add(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new MessageAttachmentAfterInsertEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
