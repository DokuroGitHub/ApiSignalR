using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using MediatR;

namespace Application.MediatR.MessageEmotes.Commands.CreateMessageEmote;

#pragma warning disable
public record CreateMessageEmoteCommand : IRequest<object>, IMapFrom<MessageEmote>
{
    public int MessageId { get; init; }
    public string? FileUrl { get; init; }
    public string? ThumbUrl { get; init; }
    public AttachmentType? Type { get; init; }
    //* ref
    // map
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateMessageEmoteCommand, MessageEmote>();
    }
}
#pragma warning restore

public class CreateMessageEmoteCommandHandler : IRequestHandler<CreateMessageEmoteCommand, object>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateMessageEmoteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<object> Handle(CreateMessageEmoteCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<MessageEmote>(request);
        entity.AddDomainEvent(new MessageEmoteBeforeInsertEvent(entity));
        _unitOfWork.MessageEmoteRepository.Add(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new MessageEmoteAfterInsertEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new { entity.MessageId, entity.UserId };
    }
}
