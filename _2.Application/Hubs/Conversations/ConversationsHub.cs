using Application.Common.Models;
using Application.MediatR.Conversations.Commands.CreateConversation;
using Application.MediatR.Conversations.Commands.DeleteConversation;
using Application.MediatR.Conversations.Commands.UpdateConversation;
using Application.MediatR.Conversations.Queries.GetConversationByKey;
using Application.MediatR.Conversations.Queries.GetConversations;
using Application.MediatR.Conversations.Queries.GetPagedConversations;
using MediatR;

namespace Application.Hubs.Conversations;

public class ConversationsHub : BaseHub // treat this as a Controller
{
    public ConversationsHub(ISender mediator) : base(mediator)
    {
    }

    public Task<PagedList<MediatR.Conversations.Queries.GetPagedConversations.ConversationBriefDto>> GetPagedConversations(GetPagedConversationsQuery query)
    => Mediator.Send(query);

    public Task<IReadOnlyCollection<MediatR.Conversations.Queries.GetConversations.ConversationBriefDto>> GetAll()
    => Mediator.Send(new GetConversationsQuery());

    public Task<MediatR.Conversations.Queries.GetConversationByKey.ConversationBriefDto> GetByKey(int id)
    => Mediator.Send(new GetConversationByKeyQuery() { Id = id });

    public Task<int> Create(CreateConversationCommand command)
    => Mediator.Send(command);

    public async Task<int> Update(int id, UpdateConversationCommand command)
    {
        command.Id = id;
        await Mediator.Send(command);
        return id;
    }

    public async Task<int> Delete(int id)
    {
        await Mediator.Send(new DeleteConversationCommand(id));
        return id;
    }
}
