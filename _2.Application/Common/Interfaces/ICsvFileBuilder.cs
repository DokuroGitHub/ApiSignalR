using Application.MediatR.TodoLists.Queries.ExportTodos;

namespace Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
