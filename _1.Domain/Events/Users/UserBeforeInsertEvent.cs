namespace Domain.Events;

public class UserBeforeInsertEvent : BaseEvent
{
    public UserBeforeInsertEvent(User item)
    {
        Item = item;
    }

    public User Item { get; }
}
