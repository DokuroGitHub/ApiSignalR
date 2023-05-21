namespace Domain.Events.SampleUsers;

public class SampleUserBeforeUpdateOfPasswordEvent : BaseEvent
{
    public SampleUserBeforeUpdateOfPasswordEvent(SampleUser item)
    {
        Item = item;
    }

    public SampleUser Item { get; }
}
