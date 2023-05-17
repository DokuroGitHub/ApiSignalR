using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.MediatR.Users.Queries.GetRoleByUserId;

public record GetRoleByUserIdQuery(int UserId) : IRequest<string>;

public class GetRoleByUserIdQueryHandler : IRequestHandler<GetRoleByUserIdQuery, string>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRoleByUserIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(GetRoleByUserIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.UserRepository.SingleOrDefaultAsync<UserBriefDto>(
            where: x => x.Id == request.UserId,
            cancellationToken: cancellationToken);
        if (result is null)
        {
            throw new NotFoundException(nameof(User), request.UserId);
        }
        return result.Role;
    }
}
