namespace Domain.Events;

public class UserBeforeDeleteEvent : BaseEvent
{
    public UserBeforeDeleteEvent(User item)
    {
        Item = item;
    }

    public User Item { get; }
}
