using AutoMapper;
using Application.Common.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.MediatR.TodoLists.Queries.GetTodos;

// [Authorize]
public record GetTodosQuery : IRequest<TodosVm>;

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, TodosVm>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetTodosQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TodosVm> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        var todoList = await _unitOfWork.TodoListRepository.GetAllAsync<TodoListDto>(
            orderBy: x => x.OrderBy(x => x.Title),
             cancellationToken: cancellationToken);
        var result = new TodosVm
        {
            PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                .Cast<PriorityLevel>()
                .Select(p => new PriorityLevelDto { Value = (int)p, Name = p.ToString() })
                .ToList(),

            Lists = todoList
        };
        return result;
    }
}
