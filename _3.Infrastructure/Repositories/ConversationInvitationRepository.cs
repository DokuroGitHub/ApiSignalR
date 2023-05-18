using Application.Common.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class ConversationInvitationRepository : GenericRepository<ConversationInvitation>, IConversationInvitationRepository
{
    private readonly ApplicationDbContext _context;

    public ConversationInvitationRepository(
        ApplicationDbContext context,
        IMapper mapper)
        : base(
            context,
            mapper)
    {
        _context = context;
    }
}
