namespace Domain.Events;

public class DeletedMessageAfterDeleteEvent : BaseEvent
{
    public DeletedMessageAfterDeleteEvent(DeletedMessage item)
    {
        Item = item;
    }

    public DeletedMessage Item { get; }
}
