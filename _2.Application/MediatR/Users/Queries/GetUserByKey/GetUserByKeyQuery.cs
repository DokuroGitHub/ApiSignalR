using Application.Common.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Domain.Entities;

namespace Application.MediatR.Users.Queries.GetUserByKey;

// [Authorize]
public record GetUserByKeyQuery : IRequest<UserBriefDto>
{
    public int Id { get; init; }
};

public class GetUserByKeyQueryHandler : IRequestHandler<GetUserByKeyQuery, UserBriefDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByKeyQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserBriefDto?> Handle(GetUserByKeyQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.UserRepository.FindAsync<UserBriefDto>(
            keyValues: request.Id,
            cancellationToken: cancellationToken);
        if (result is null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }
        return result;
    }
}
