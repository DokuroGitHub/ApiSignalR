using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using MediatR;

namespace Application.MediatR.MessageEmotes.Commands.UpdateMessageEmote;

public record UpdateMessageEmoteCommand : IRequest, IMapFrom<MessageEmote>
{
    public int MessageId { get; set; }
    public int UserId { get; set; }
    public EmoteCode Code { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateMessageEmoteCommand, MessageEmote>()
            .ForMember(des => des.MessageId, opt => opt.Ignore())
            .ForMember(des => des.UserId, opt => opt.Ignore());
    }
}

public class UpdateMessageEmoteCommandHandler : IRequestHandler<UpdateMessageEmoteCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateMessageEmoteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(UpdateMessageEmoteCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.MessageEmoteRepository.SingleOrDefaultAsync(
            where: x => x.MessageId == request.MessageId && x.UserId == request.UserId,
            tracked: true,
            cancellationToken: cancellationToken);

        if (entity is null)
            throw new NotFoundException(nameof(MessageEmote), new { request.MessageId, request.UserId });
        _mapper.Map(request, entity);
        entity.AddDomainEvent(new MessageEmoteBeforeUpdateEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new MessageEmoteAfterUpdateEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
