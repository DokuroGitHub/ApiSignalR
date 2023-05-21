namespace Domain.Events.Users;

public class UserBeforeUpdateEvent : BaseEvent
{
    public UserBeforeUpdateEvent(User item)
    {
        Item = item;
    }

    public User Item { get; }
}
