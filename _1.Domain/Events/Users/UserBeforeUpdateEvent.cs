namespace Domain.Events;

public class UserBeforeUpdateEvent : BaseEvent
{
    public UserBeforeUpdateEvent(User item)
    {
        Item = item;
    }

    public User Item { get; }
}
