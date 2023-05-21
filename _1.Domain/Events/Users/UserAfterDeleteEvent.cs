namespace Domain.Events.Users;

public class UserAfterDeleteEvent : BaseEvent
{
    public UserAfterDeleteEvent(User item)
    {
        Item = item;
    }

    public User Item { get; }
}
