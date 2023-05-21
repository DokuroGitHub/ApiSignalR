using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Events.SampleUsers;
using MediatR;

namespace Application.MediatR.SampleUsers.Commands.CreateSampleUser;

#pragma warning disable
public record CreateSampleUserCommand : IRequest<int>, IMapFrom<SampleUser>
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
    public string SampleUsername { get; init; }
    public string Password { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateSampleUserCommand, SampleUser>()
            .ForMember(d => d.PasswordHash, opt => opt.MapFrom(s => s.Password.Hash()));
    }
}

public class CreateSampleUserCommandHandler : IRequestHandler<CreateSampleUserCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateSampleUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateSampleUserCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SampleUser>(request);
        entity.AddDomainEvent(new SampleUserBeforeInsertEvent(entity));
        _unitOfWork.SampleUserRepository.Add(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        entity.AddDomainEvent(new SampleUserAfterInsertEvent(entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
