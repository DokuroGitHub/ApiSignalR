namespace Domain.Events;

public class DeletedMessageAfterUpdateEvent : BaseEvent
{
    public DeletedMessageAfterUpdateEvent(DeletedMessage item)
    {
        Item = item;
    }

    public DeletedMessage Item { get; }
}
