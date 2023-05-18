using Application.Common.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class ConversationRepository : GenericRepository<Conversation>, IConversationRepository
{
    private readonly ApplicationDbContext _context;

    public ConversationRepository(
        ApplicationDbContext context,
        IMapper mapper)
        : base(
            context,
            mapper)
    {
        _context = context;
    }
}
