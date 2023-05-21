using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Events.SampleUsers;
using MediatR;

namespace Application.MediatR.SampleUsers.Commands.UpdateSampleUser;

public record UpdateSampleUserCommand : IRequest, IMapFrom<SampleUser>
{
    public int Id { get; set; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateSampleUserCommand, SampleUser>()
            .ForMember(des => des.Id, opt => opt.Ignore())
            .ForMember(des => des.FirstName, opt => opt.Condition(src => src.FirstName != null))
            .ForMember(des => des.LastName, opt => opt.Condition(src => src.LastName != null));
    }
}

public class UpdateSampleUserCommandHandler : IRequestHandler<UpdateSampleUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateSampleUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(UpdateSampleUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.SampleUserRepository.SingleOrDefaultAsync(
            where: x => x.Id == request.Id,
            tracked: true,
            cancellationToken: cancellationToken);

        if (entity is null)
            throw new NotFoundException(nameof(SampleUser), request.Id);
        _mapper.Map(request, entity);
        entity.AddDomainEvent(new SampleUserBeforeUpdateEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new SampleUserAfterUpdateEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
