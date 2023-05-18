using Application.Common.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class ConversationBlockRepository : GenericRepository<ConversationBlock>, IConversationBlockRepository
{
    private readonly ApplicationDbContext _context;

    public ConversationBlockRepository(
        ApplicationDbContext context,
        IMapper mapper)
        : base(
            context,
            mapper)
    {
        _context = context;
    }
}
