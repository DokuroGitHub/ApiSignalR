
using Application.Common.Interfaces.IRepositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class TodoListRepository : GenericRepository<TodoList>, ITodoListRepository
{
    private readonly ApplicationDbContext _context;

    public TodoListRepository(
        ApplicationDbContext context,
        IMapper mapper)
        : base(
            context,
            mapper)
    {
        _context = context;
    }
}
