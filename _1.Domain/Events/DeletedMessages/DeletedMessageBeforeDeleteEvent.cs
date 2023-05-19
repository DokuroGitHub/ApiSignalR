namespace Domain.Events;

public class DeletedMessageBeforeDeleteEvent : BaseEvent
{
    public DeletedMessageBeforeDeleteEvent(DeletedMessage item)
    {
        Item = item;
    }

    public DeletedMessage Item { get; }
}
