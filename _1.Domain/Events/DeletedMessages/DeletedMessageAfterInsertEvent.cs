namespace Domain.Events;

public class DeletedMessageAfterInsertEvent : BaseEvent
{
    public DeletedMessageAfterInsertEvent(DeletedMessage item)
    {
        Item = item;
    }

    public DeletedMessage Item { get; }
}
