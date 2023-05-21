namespace Domain.Events.Users;

public class UserBeforeInsertEvent : BaseEvent
{
    public UserBeforeInsertEvent(User item)
    {
        Item = item;
    }

    public User Item { get; }
}
