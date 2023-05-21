using Application.Common.Interfaces;
using Application.Common.Interfaces.AuthThirdParty;
using Application.Common.Models;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Domain.Events.Users;
using MediatR;

namespace Application.MediatR.Auth.Commands.LoginThirdParty;

public record LoginThirdPartyCommand : IRequest<LoginResponse>
{
#pragma warning disable
    public string Email { get; init; }
    public string Password { get; init; }
    public string? PasswordConfirm { get; init; }
};

public class LoginQueryHandler : IRequestHandler<LoginThirdPartyCommand, LoginResponse>
{
    private readonly IAuthThirdPartyService _authThirtPartyService;
    private readonly IJwtService _jwtService;
    private readonly IUnitOfWork _unitOfWork;

    public LoginQueryHandler(
        IAuthThirdPartyService authThirtPartyService,
        IJwtService jwtService,
        IUnitOfWork unitOfWork)
    {
        _authThirtPartyService = authThirtPartyService;
        _jwtService = jwtService;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginResponse> Handle(LoginThirdPartyCommand request, CancellationToken cancellationToken)
    {
        var response = await _authThirtPartyService.Login(new LoginRequest()
        {
            Email = request.Email,
            Password = request.Password,
        });

        var userId = $"UserId_{response.Id}";

        var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync(
            where: x => x.UserId == userId,
            cancellationToken: cancellationToken);

        // if not found, create new user
        if (user is null)
        {
            user = new User()
            {
                UserId = userId,
                Email = request.Email,
                Username = request.Email,
                PasswordHash = request.Password.Hash(),
                Role = UserRole.User,
                Token = response.Token,
                FirstName = response.Name,
            };

            user.AddDomainEvent(new UserBeforeInsertEvent(user));
            _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            user.AddDomainEvent(new UserAfterInsertEvent(user));
        }
        else
        {
            // if found, update user
            user.Token = response.Token;
            user.AddDomainEvent(new UserBeforeUpdateEvent(user));
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            user.AddDomainEvent(new UserAfterUpdateEvent(user));
        }

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
