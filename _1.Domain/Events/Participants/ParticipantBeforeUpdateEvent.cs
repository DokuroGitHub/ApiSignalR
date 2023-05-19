namespace Domain.Events;

public class ParticipantBeforeUpdateEvent : BaseEvent
{
    public ParticipantBeforeUpdateEvent(Participant item)
    {
        Item = item;
    }

    public Participant Item { get; }
}
