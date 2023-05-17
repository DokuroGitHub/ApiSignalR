namespace Domain.Events;

public class UserAfterUpdateEvent : BaseEvent
{
    public UserAfterUpdateEvent(User item)
    {
        Item = item;
    }

    public User Item { get; }
}
