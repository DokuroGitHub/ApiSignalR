namespace Domain.Events;

public class UserAfterDeleteEvent : BaseEvent
{
    public UserAfterDeleteEvent(User item)
    {
        Item = item;
    }

    public User Item { get; }
}
