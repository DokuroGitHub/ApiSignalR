using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.MediatR.Conversations.Commands.UpdateConversation;

public record UpdateConversationCommand : IRequest, IMapFrom<Conversation>
{
    public int Id { get; set; }
    public string? Title { get; init; }
    public string? Description { get; init; }
    public string? PhotoUrl { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateConversationCommand, Conversation>()
            .ForMember(des => des.Id, opt => opt.Ignore())
            .ForMember(des => des.Title, opt => opt.Condition(src => src.Title != null))
            .ForMember(des => des.Description, opt => opt.Condition(src => src.Description != null))
            .ForMember(des => des.PhotoUrl, opt => opt.Condition(src => src.PhotoUrl != null));
    }
}

public class UpdateConversationCommandHandler : IRequestHandler<UpdateConversationCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateConversationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(UpdateConversationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.ConversationRepository.SingleOrDefaultAsync(
            where: x => x.Id == request.Id,
            tracked: true,
            cancellationToken: cancellationToken);

        if (entity is null)
            throw new NotFoundException(nameof(Conversation), request.Id);
        _mapper.Map(request, entity);
        entity.AddDomainEvent(new ConversationBeforeUpdateEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new ConversationAfterUpdateEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
