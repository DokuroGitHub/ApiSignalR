using Application.Common.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class ParticipantRepository : GenericRepository<Participant>, IParticipantRepository
{
    private readonly ApplicationDbContext _context;

    public ParticipantRepository(
        ApplicationDbContext context,
        IMapper mapper)
        : base(
            context,
            mapper)
    {
        _context = context;
    }
}
