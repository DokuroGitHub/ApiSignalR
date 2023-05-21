using Application.Common.Interfaces;
using MediatR;
using Application.Common.Exceptions;
using Domain.Entities;

namespace Application.MediatR.SampleUsers.Queries.GetSampleUserByKey;

// [Authorize]
public record GetSampleUserByKeyQuery : IRequest<SampleUserBriefDto>
{
    public int Id { get; init; }
};

public class GetSampleUserByKeyQueryHandler : IRequestHandler<GetSampleUserByKeyQuery, SampleUserBriefDto?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetSampleUserByKeyQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<SampleUserBriefDto?> Handle(GetSampleUserByKeyQuery request, CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.SampleUserRepository.FindAsync<SampleUserBriefDto>(
            keyValues: request.Id,
            cancellationToken: cancellationToken);
        if (result is null)
        {
            throw new NotFoundException(nameof(SampleUser), request.Id);
        }
        return result;
    }
}
