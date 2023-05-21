using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using MediatR;

namespace Application.MediatR.Participants.Commands.CreateParticipant;

#pragma warning disable
public record CreateParticipantCommand : IRequest, IMapFrom<Participant>
{
    public int ConversationId { get; init; }
    public int UserId { get; init; }
    public string? Nickname { get; init; }
    public ConversationRole? Role { get; init; }
    //* ref
    // map
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateParticipantCommand, Participant>();
    }
}
#pragma warning restore

public class CreateParticipantCommandHandler : IRequestHandler<CreateParticipantCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateParticipantCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(CreateParticipantCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Participant>(request);
        entity.AddDomainEvent(new ParticipantBeforeInsertEvent(entity));
        _unitOfWork.ParticipantRepository.Add(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new ParticipantAfterInsertEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
