namespace Domain.Events;

public class ParticipantAfterDeleteEvent : BaseEvent
{
    public ParticipantAfterDeleteEvent(Participant item)
    {
        Item = item;
    }

    public Participant Item { get; }
}
