using Application.Common.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class MessageEmoteRepository : GenericRepository<MessageEmote>, IMessageEmoteRepository
{
    private readonly ApplicationDbContext _context;

    public MessageEmoteRepository(
        ApplicationDbContext context,
        IMapper mapper)
        : base(
            context,
            mapper)
    {
        _context = context;
    }
}
