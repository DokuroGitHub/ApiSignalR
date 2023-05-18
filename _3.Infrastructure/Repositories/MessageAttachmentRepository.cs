using Application.Common.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class MessageAttachmentRepository : GenericRepository<MessageAttachment>, IMessageAttachmentRepository
{
    private readonly ApplicationDbContext _context;

    public MessageAttachmentRepository(
        ApplicationDbContext context,
        IMapper mapper)
        : base(
            context,
            mapper)
    {
        _context = context;
    }
}
