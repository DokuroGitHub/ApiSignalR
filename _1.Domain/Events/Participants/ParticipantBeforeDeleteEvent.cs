namespace Domain.Events;

public class ParticipantBeforeDeleteEvent : BaseEvent
{
    public ParticipantBeforeDeleteEvent(Participant item)
    {
        Item = item;
    }

    public Participant Item { get; }
}
