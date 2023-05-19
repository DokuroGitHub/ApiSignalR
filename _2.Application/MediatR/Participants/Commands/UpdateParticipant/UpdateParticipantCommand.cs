using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using MediatR;

namespace Application.MediatR.Participants.Commands.UpdateParticipant;

#pragma warning disable
public record UpdateParticipantCommand : IRequest, IMapFrom<Participant>
{
    public int ConversationId { get; set; }
    public int UserId { get; set; }
    public string? Nickname { get; init; }
    public ConversationRole? Role { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateParticipantCommand, Participant>()
            .ForMember(des => des.ConversationId, opt => opt.Ignore())
            .ForMember(des => des.UserId, opt => opt.Ignore())
            .ForMember(des => des.Nickname, opt => opt.Condition(src => src.Nickname != null))
            .ForMember(des => des.Role, opt => opt.Condition(src => src.Role != null));
    }
}
#pragma warning restore

public class UpdateParticipantCommandHandler : IRequestHandler<UpdateParticipantCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateParticipantCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(UpdateParticipantCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.ParticipantRepository.SingleAsync(
            where: x => x.ConversationId == request.ConversationId && x.UserId == request.UserId,
            tracked: true,
            cancellationToken: cancellationToken);
        _mapper.Map(request, entity);
        entity.AddDomainEvent(new ParticipantBeforeUpdateEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new ParticipantAfterUpdateEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
