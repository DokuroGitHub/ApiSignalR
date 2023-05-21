using Application.Common.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Application.Common.Models;
using FluentValidation.Results;
using Domain.Enums;

namespace Application.MediatR.Auth.Queries.LoginAdmin;

public record LoginAdminQuery : IRequest<LoginResponse>
{
#pragma warning disable
    public string Username { get; init; }
    public string Password { get; init; }
    public string? PasswordConfirm { get; init; }
};

public class LoginAdminQueryHandler : IRequestHandler<LoginAdminQuery, LoginResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public LoginAdminQueryHandler(
        IUnitOfWork unitOfWork,
        IJwtService jwtService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> Handle(LoginAdminQuery request, CancellationToken cancellationToken)
    {
        if (request.Username != "admin" || request.Password != "admin")
        {
            throw new ValidationException(new List<ValidationFailure>(){
                new ValidationFailure("Login", "Login failed, please check your credentials."),
            });
        };
        var currentUser = new CurrentUser()
        {
            UserId = int.MaxValue,
            DisplayName = "admin",
            Email = "admin@gmail.com",
            Role = UserRole.Admin,
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
