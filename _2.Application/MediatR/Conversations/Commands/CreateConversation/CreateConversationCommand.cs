using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using MediatR;

namespace Application.MediatR.Conversations.Commands.CreateConversation;

public record CreateConversationCommand : IRequest<int>, IMapFrom<Conversation>
{
#pragma warning disable
    public string? Title { get; init; }
    public string? Description { get; init; }
    public string? PhotoUrl { get; init; }
    // ref
    public virtual ICollection<MessageBriefDto>? Messages { get; init; }
    public virtual ICollection<ConversationInvitationBriefDto>? Invitations { get; init; }
    public virtual ICollection<ParticipantBriefDto>? Participants { get; init; }
}

public record MessageBriefDto
{
#pragma warning disable
    public string? Content { get; init; }
    // ref
    public virtual ICollection<MessageAttachmentBriefDto> Attachments { get; init; }
}

public record MessageAttachmentBriefDto
{
#pragma warning disable
    public string? FileUrl { get; set; }
    public string? ThumbUrl { get; set; }
    public AttachmentType Type { get; set; }
    // ref
}

public record ConversationInvitationBriefDto
{
    public int UserId { get; set; }
    public ConversationRole Role { get; set; }
    // ref
}

public record ParticipantBriefDto
{
    public int UserId { get; set; }
    public ConversationRole Role { get; set; }
    // ref
}

public class CreateConversationCommandHandler : IRequestHandler<CreateConversationCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateConversationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Conversation>(request);
        entity.AddDomainEvent(new ConversationBeforeInsertEvent(entity));
        _unitOfWork.ConversationRepository.Add(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new ConversationAfterInsertEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
