namespace Domain.Events.SampleUsers;

public class SampleUserBeforeUpdateEvent : BaseEvent
{
    public SampleUserBeforeUpdateEvent(SampleUser item)
    {
        Item = item;
    }

    public SampleUser Item { get; }
}
