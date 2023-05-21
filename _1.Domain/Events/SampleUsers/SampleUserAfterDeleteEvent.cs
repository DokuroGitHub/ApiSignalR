namespace Domain.Events.SampleUsers;

public class SampleUserAfterDeleteEvent : BaseEvent
{
    public SampleUserAfterDeleteEvent(SampleUser item)
    {
        Item = item;
    }

    public SampleUser Item { get; }
}
