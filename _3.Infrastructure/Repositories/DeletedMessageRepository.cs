using Application.Common.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class DeletedMessageRepository : GenericRepository<DeletedMessage>, IDeletedMessageRepository
{
    private readonly ApplicationDbContext _context;

    public DeletedMessageRepository(
        ApplicationDbContext context,
        IMapper mapper)
        : base(
            context,
            mapper)
    {
        _context = context;
    }
}
