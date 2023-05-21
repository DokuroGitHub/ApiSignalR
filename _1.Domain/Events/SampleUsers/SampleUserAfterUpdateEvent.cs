namespace Domain.Events.SampleUsers;

public class SampleUserAfterUpdateEvent : BaseEvent
{
    public SampleUserAfterUpdateEvent(SampleUser item)
    {
        Item = item;
    }

    public SampleUser Item { get; }
}
