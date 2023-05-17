namespace Domain.Events;

public class UserBeforeUpdateOfPasswordEvent : BaseEvent
{
    public UserBeforeUpdateOfPasswordEvent(User item)
    {
        Item = item;
    }

    public User Item { get; }
}
