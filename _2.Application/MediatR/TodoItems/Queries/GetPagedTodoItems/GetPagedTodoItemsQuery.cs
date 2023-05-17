using AutoMapper;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.MediatR.TodoItems.Queries.GetPagedTodoItems;

public record GetPagedTodoItemsQuery : IRequest<PagedList<TodoItemBriefDto>>
{
    public int ListId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetPagedTodoItemsQueryHandler : IRequestHandler<GetPagedTodoItemsQuery, PagedList<TodoItemBriefDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPagedTodoItemsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedList<TodoItemBriefDto>> Handle(
        GetPagedTodoItemsQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _unitOfWork.TodoItemRepository.GetPageListAsync<TodoItemBriefDto>(
            where: x => x.ListId == request.ListId,
            orderBy: x => x.OrderBy(x => x.Title),
            pageNumber: request.PageNumber,
            pageSize: request.PageSize);
        return result;
    }
}
