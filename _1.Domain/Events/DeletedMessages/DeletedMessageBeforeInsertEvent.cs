namespace Domain.Events;

public class DeletedMessageBeforeInsertEvent : BaseEvent
{
    public DeletedMessageBeforeInsertEvent(DeletedMessage item)
    {
        Item = item;
    }

    public DeletedMessage Item { get; }
}
