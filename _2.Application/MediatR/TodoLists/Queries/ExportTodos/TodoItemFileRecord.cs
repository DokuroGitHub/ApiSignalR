using Application.Common.Mappings;
using Domain.Entities;

namespace Application.MediatR.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; init; }

    public bool Done { get; init; }
}
