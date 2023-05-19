namespace Domain.Events;

public class DeletedMessageBeforeUpdateEvent : BaseEvent
{
    public DeletedMessageBeforeUpdateEvent(DeletedMessage item)
    {
        Item = item;
    }

    public DeletedMessage Item { get; }
}
