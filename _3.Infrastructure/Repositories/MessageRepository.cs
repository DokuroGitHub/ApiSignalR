using Application.Common.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class MessageRepository : GenericRepository<Message>, IMessageRepository
{
    private readonly ApplicationDbContext _context;

    public MessageRepository(
        ApplicationDbContext context,
        IMapper mapper)
        : base(
            context,
            mapper)
    {
        _context = context;
    }
}
