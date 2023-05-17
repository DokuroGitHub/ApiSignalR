
using Application.Common.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class TodoItemRepository : GenericRepository<TodoItem>, ITodoItemRepository
{
    private readonly ApplicationDbContext _context;

    public TodoItemRepository(
        ApplicationDbContext context,
        IMapper mapper)
        : base(
            context,
            mapper)
    {
        _context = context;
    }
}
