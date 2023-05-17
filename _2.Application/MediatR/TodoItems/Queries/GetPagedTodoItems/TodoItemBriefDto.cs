using Application.Common.Mappings;
using Domain.Entities;

namespace Application.MediatR.TodoItems.Queries.GetPagedTodoItems;

public class TodoItemBriefDto : IMapFrom<TodoItem>
{
    public int Id { get; init; }

    public int ListId { get; init; }

    // public string? Title { get; init; }

    public bool Done { get; init; }
}
