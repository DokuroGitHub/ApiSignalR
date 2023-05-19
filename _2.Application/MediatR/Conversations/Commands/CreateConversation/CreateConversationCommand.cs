using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using MediatR;

namespace Application.MediatR.Conversations.Commands.CreateConversation;

#pragma warning disable
public record CreateConversationCommand : IRequest<int>, IMapFrom<Conversation>
{
    public string? Title { get; init; }
    public string? Description { get; init; }
    public string? PhotoUrl { get; init; }
    // ref
    public ICollection<MessageBriefDto>? Messages { get; init; }
    public ICollection<ConversationInvitationBriefDto>? Invitations { get; init; }
    public ICollection<ParticipantBriefDto>? Participants { get; init; }
    // map
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateConversationCommand, Conversation>();
    }
}

public class MessageBriefDto : IMapFrom<Message>
{
    public string? Content { get; init; }
    // ref
    public ICollection<MessageAttachmentBriefDto> Attachments { get; init; }
    // map
    public void Mapping(Profile profile)
    {
        profile.CreateMap<MessageBriefDto, Message>();
    }
}

public class MessageAttachmentBriefDto : IMapFrom<MessageAttachment>
{
    public string? FileUrl { get; init; }
    public string? ThumbUrl { get; init; }
    public AttachmentType Type { get; init; }
    // ref
    // map
    public void Mapping(Profile profile)
    {
        profile.CreateMap<MessageAttachmentBriefDto, MessageAttachment>();
    }
}

public class ConversationInvitationBriefDto : IMapFrom<ConversationInvitation>
{
    public int UserId { get; init; }
    public ConversationRole Role { get; init; }
    // ref
    // map
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ConversationInvitationBriefDto, ConversationInvitation>();
    }
}

public class ParticipantBriefDto : IMapFrom<Participant>
{
    public int UserId { get; init; }
    public ConversationRole Role { get; init; }
    // ref
    // map
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ParticipantBriefDto, Participant>();
    }
}
#pragma warning restore

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
