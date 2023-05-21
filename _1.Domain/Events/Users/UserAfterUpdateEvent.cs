namespace Domain.Events.Users;

public class UserAfterUpdateEvent : BaseEvent
{
    public UserAfterUpdateEvent(User item)
    {
        Item = item;
    }

    public User Item { get; }
}
