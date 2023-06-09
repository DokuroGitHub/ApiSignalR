﻿using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using MediatR;

namespace Application.MediatR.Messages.Commands.UpdateMessage;

#pragma warning disable
public record UpdateMessageCommand : IRequest, IMapFrom<Message>
{
    public int Id { get; set; }
    public string? Content { get; init; }
    public ICollection<UpdateMessageAttachmentDto>? Attachments { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateMessageCommand, Message>()
            .ForMember(des => des.Id, opt => opt.Ignore())
            .ForMember(des => des.Content, opt => opt.Condition(src => src.Content != null));
    }
}

public class UpdateMessageAttachmentDto : IMapFrom<MessageAttachment>
{
    public string? FileUrl { get; init; }
    public string? ThumbUrl { get; init; }
    public AttachmentType Type { get; init; }
    //* ref
    // map
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateMessageAttachmentDto, MessageAttachment>();
    }
}
#pragma warning restore

public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateMessageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.MessageRepository.SingleOrDefaultAsync(
            where: x => x.Id == request.Id,
            tracked: true,
            cancellationToken: cancellationToken);

        if (entity is null)
            throw new NotFoundException(nameof(Message), request.Id);
        _mapper.Map(request, entity);
        entity.AddDomainEvent(new MessageBeforeUpdateEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new MessageAfterUpdateEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
