using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.MediatR.Auth.Commands.Register;

#pragma warning disable
public record RegisterCommand : IRequest<int>, IMapFrom<User>
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<RegisterCommand, User>()
            .ForMember(d => d.PasswordHash, opt => opt.MapFrom(s => s.Password.Hash()));
    }
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<User>(request);
        entity.AddDomainEvent(new UserBeforeInsertEvent(entity));
        _unitOfWork.UserRepository.Add(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new UserAfterInsertEvent(entity));
        return entity.Id;
    }
}
