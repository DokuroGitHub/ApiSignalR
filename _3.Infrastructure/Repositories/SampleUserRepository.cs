using Application.Common.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class SampleUserRepository : GenericRepository<SampleUser>, ISampleUserRepository
{
    private readonly ApplicationDbContext _context;

    public SampleUserRepository(
        ApplicationDbContext context,
        IMapper mapper)
        : base(
            context,
            mapper)
    {
        _context = context;
    }
}
