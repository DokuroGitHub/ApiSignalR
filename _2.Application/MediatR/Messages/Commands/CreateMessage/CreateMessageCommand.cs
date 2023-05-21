using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using MediatR;

namespace Application.MediatR.Messages.Commands.CreateMessage;

#pragma warning disable
public record CreateMessageCommand : IRequest<int>, IMapFrom<Message>
{
    public int ConversationId { get; init; }
    public string? Content { get; init; }
    public int? ReplyTo { get; init; }
    //* ref
    public ICollection<CreateMessageAttachmentDto>? Attachments { get; init; }
    // map
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateMessageCommand, Message>();
    }
}

public class CreateMessageAttachmentDto : IMapFrom<MessageAttachment>
{
    public string? FileUrl { get; init; }
    public string? ThumbUrl { get; init; }
    public AttachmentType Type { get; init; }
    //* ref
    // map
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateMessageAttachmentDto, MessageAttachment>();
    }
}
#pragma warning restore

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateMessageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Message>(request);
        entity.AddDomainEvent(new MessageBeforeInsertEvent(entity));
        _unitOfWork.MessageRepository.Add(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new MessageAfterInsertEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
