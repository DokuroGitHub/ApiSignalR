namespace Domain.Events.Users;

public class UserBeforeUpdateOfPasswordEvent : BaseEvent
{
    public UserBeforeUpdateOfPasswordEvent(User item)
    {
        Item = item;
    }

    public User Item { get; }
}
