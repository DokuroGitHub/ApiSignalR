using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.MediatR.Users.Commands.UpdateUser;

public record UpdateUserCommand : IRequest, IMapFrom<User>
{
    public int Id { get; set; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateUserCommand, User>()
            .ForMember(des => des.Id, opt => opt.Ignore())
            .ForMember(des => des.FirstName, opt => opt.Condition(src => src.FirstName != null))
            .ForMember(des => des.LastName, opt => opt.Condition(src => src.LastName != null));
    }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.UserRepository.SingleOrDefaultAsync(
            where: x => x.Id == request.Id,
            tracked: true,
            cancellationToken: cancellationToken);

        if (entity is null)
            throw new NotFoundException(nameof(User), request.Id);
        _mapper.Map(request, entity);
        entity.AddDomainEvent(new UserBeforeUpdateEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new UserAfterUpdateEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
