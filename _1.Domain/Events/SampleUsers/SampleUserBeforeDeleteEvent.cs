namespace Domain.Events.SampleUsers;

public class SampleUserBeforeDeleteEvent : BaseEvent
{
    public SampleUserBeforeDeleteEvent(SampleUser item)
    {
        Item = item;
    }

    public SampleUser Item { get; }
}
