using AutoMapper;
using AutoMapper.QueryableExtensions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MediatR.TodoLists.Queries.ExportTodos;

public record ExportTodosQuery : IRequest<ExportTodosVm>
{
    public int ListId { get; init; }
}

public class ExportTodosQueryHandler : IRequestHandler<ExportTodosQuery, ExportTodosVm>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICsvFileBuilder _fileBuilder;

    public ExportTodosQueryHandler(IUnitOfWork unitOfWork, ICsvFileBuilder fileBuilder)
    {
        _unitOfWork = unitOfWork;
        _fileBuilder = fileBuilder;
    }

    public async Task<ExportTodosVm> Handle(ExportTodosQuery request, CancellationToken cancellationToken)
    {
        var records = await _unitOfWork.TodoItemRepository.GetAllAsync<TodoItemRecord>(
            where: t => t.ListId == request.ListId,
            cancellationToken: cancellationToken);

        var vm = new ExportTodosVm(
            "TodoItems.csv",
            "text/csv",
            _fileBuilder.BuildTodoItemsFile(records));

        return vm;
    }
}
