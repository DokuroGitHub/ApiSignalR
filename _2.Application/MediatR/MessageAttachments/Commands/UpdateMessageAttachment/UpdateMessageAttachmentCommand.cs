using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using MediatR;

namespace Application.MediatR.MessageAttachments.Commands.UpdateMessageAttachment;

public record UpdateMessageAttachmentCommand : IRequest, IMapFrom<MessageAttachment>
{
    public int Id { get; set; }
    public string? FileUrl { get; init; }
    public string? ThumbUrl { get; init; }
    public AttachmentType? Type { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateMessageAttachmentCommand, MessageAttachment>()
            .ForMember(des => des.Id, opt => opt.Ignore())
            .ForMember(des => des.FileUrl, opt => opt.Condition(src => src.FileUrl != null))
            .ForMember(des => des.ThumbUrl, opt => opt.Condition(src => src.ThumbUrl != null))
            .ForMember(des => des.Type, opt => opt.Condition(src => src.Type != null));
    }
}

public class UpdateMessageAttachmentCommandHandler : IRequestHandler<UpdateMessageAttachmentCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateMessageAttachmentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(UpdateMessageAttachmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.MessageAttachmentRepository.SingleOrDefaultAsync(
            where: x => x.Id == request.Id,
            tracked: true,
            cancellationToken: cancellationToken);

        if (entity is null)
            throw new NotFoundException(nameof(MessageAttachment), request.Id);
        _mapper.Map(request, entity);
        entity.AddDomainEvent(new MessageAttachmentBeforeUpdateEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new MessageAttachmentAfterUpdateEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
