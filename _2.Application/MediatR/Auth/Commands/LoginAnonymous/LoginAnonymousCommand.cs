using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Events.Users;
using MediatR;

namespace Application.MediatR.Auth.Commands.LoginAnonymous;

#pragma warning disable
public record LoginAnonymousCommand : IRequest<LoginResponse>, IMapFrom<User>
{
    public string Name { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<LoginAnonymousCommand, User>()
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.Name));
    }
}

public class RegisterCommandHandler : IRequestHandler<LoginAnonymousCommand, LoginResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IJwtService _jwtService;

    public RegisterCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IJwtService jwtService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> Handle(LoginAnonymousCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        user.AddDomainEvent(new UserBeforeInsertEvent(user));
        _unitOfWork.UserRepository.Add(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        user.AddDomainEvent(new UserAfterInsertEvent(user));
        var currentUser = new CurrentUser()
        {
            UserId = user.Id,
            DisplayName = user.DisplayName,
            Email = user.Email,
            Role = user.Role,
        };
        var token = _jwtService.GenerateJWT(currentUser);

        var result = new LoginResponse()
        {
            Token = token,
            CurrentUser = currentUser,
        };
        return result;
    }
}
