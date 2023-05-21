namespace Domain.Events.SampleUsers;

public class SampleUserBeforeInsertEvent : BaseEvent
{
    public SampleUserBeforeInsertEvent(SampleUser item)
    {
        Item = item;
    }

    public SampleUser Item { get; }
}
