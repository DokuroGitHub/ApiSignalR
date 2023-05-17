using MediatR;
using Domain.Common;

namespace Application.MediatR.App.Queries.GetAppsettings;

public record GetAppsettingsQuery : IRequest<Appsettings>
{
#pragma warning disable
    public string Username { get; init; }
    public string Password { get; init; }
    public string? PasswordConfirm { get; init; }
};

public class GetAppsettingsQueryHandler : IRequestHandler<GetAppsettingsQuery, Appsettings>
{
    private readonly Appsettings _appsettings;

    public GetAppsettingsQueryHandler(
        Appsettings appsettings)
    {
        _appsettings = appsettings;
    }

    public async Task<Appsettings> Handle(GetAppsettingsQuery request, CancellationToken cancellationToken)
    => _appsettings;
}
