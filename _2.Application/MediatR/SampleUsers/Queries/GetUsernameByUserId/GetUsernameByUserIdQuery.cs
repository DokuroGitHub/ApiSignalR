using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.MediatR.SampleUsers.Queries.GetSampleUsernameBySampleUserId;

public record GetSampleUsernameBySampleUserIdQuery(int SampleUserId) : IRequest<string>;

public class GetSampleUsernameBySampleUserIdQueryHandler : IRequestHandler<GetSampleUsernameBySampleUserIdQuery, string>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSampleUsernameBySampleUserIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<string> Handle(GetSampleUsernameBySampleUserIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.SampleUserRepository.SingleOrDefaultAsync<SampleUserBriefDto>(
            where: x => x.Id == request.SampleUserId,
            cancellationToken: cancellationToken);
        if (result is null)
        {
            throw new NotFoundException(nameof(SampleUser), request.SampleUserId);
        }
        return result.SampleUsername;
    }
}
