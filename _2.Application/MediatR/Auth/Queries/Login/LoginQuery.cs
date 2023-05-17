using Application.Common.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Domain.Common;
using Application.Common.Models;
using FluentValidation.Results;

namespace Application.MediatR.Auth.Queries.Login;

public record LoginQuery : IRequest<LoginResponse>
{
#pragma warning disable
    public string Username { get; init; }
    public string Password { get; init; }
    public string? PasswordConfirm { get; init; }
};

public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public LoginQueryHandler(
        IUnitOfWork unitOfWork,
        IJwtService jwtService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.SingleOrDefaultAsync<UserBriefDto>(
            where: x => x.Username == request.Username,
            cancellationToken: cancellationToken);
        if (user is null)
        {
            throw new ValidationException(new List<ValidationFailure>(){
                new ValidationFailure("Login", "Login failed, please check your credentials. (bật mí luôn là sai tên đăng nhập rồi)"),
            });
        };
        bool isValid = request.Password.Verify(user.PasswordHash);
        if (!isValid)
        {
            throw new ValidationException(new List<ValidationFailure>(){
                new ValidationFailure("Login", "Login failed, please check your credentials. (bật mí luôn là sai mật khẩu rồi)"),
            });
        };
        //if user was found generate JWT Token
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
