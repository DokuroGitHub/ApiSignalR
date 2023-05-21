namespace Domain.Events.SampleUsers;

public class SampleUserAfterInsertEvent : BaseEvent
{
    public SampleUserAfterInsertEvent(SampleUser item)
    {
        Item = item;
    }

    public SampleUser Item { get; }
}
