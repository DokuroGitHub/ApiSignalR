namespace Domain.Events;

public class ParticipantAfterUpdateEvent : BaseEvent
{
    public ParticipantAfterUpdateEvent(Participant item)
    {
        Item = item;
    }

    public Participant Item { get; }
}
