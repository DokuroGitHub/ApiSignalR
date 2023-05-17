namespace Domain.Events;

public class UserAfterInsertEvent : BaseEvent
{
    public UserAfterInsertEvent(User item)
    {
        Item = item;
    }

    public User Item { get; }
}
