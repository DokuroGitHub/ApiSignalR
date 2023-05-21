using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.MediatR.SampleUsers.Queries.GetRoleBySampleUserId;

public record GetRoleBySampleUserIdQuery(int SampleUserId) : IRequest<string>;

public class GetRoleBySampleUserIdQueryHandler : IRequestHandler<GetRoleBySampleUserIdQuery, string>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRoleBySampleUserIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(GetRoleBySampleUserIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.SampleUserRepository.SingleOrDefaultAsync<SampleUserBriefDto>(
            where: x => x.Id == request.SampleUserId,
            cancellationToken: cancellationToken);
        if (result is null)
        {
            throw new NotFoundException(nameof(SampleUser), request.SampleUserId);
        }
        return result.Role;
    }
}
