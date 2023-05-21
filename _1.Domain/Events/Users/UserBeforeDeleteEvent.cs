namespace Domain.Events.Users;

public class UserBeforeDeleteEvent : BaseEvent
{
    public UserBeforeDeleteEvent(User item)
    {
        Item = item;
    }

    public User Item { get; }
}
