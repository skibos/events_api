using Events.Application.Common.Interfaces.Persistance;
using Events.Domain.Events;
using Events.Domain.Events.Enums;
using Hangfire;

namespace Events.Application.Common.Interfaces.Workers;

public class EventWorker
{
    private readonly IEventRepository _eventRepository;

    public EventWorker(IEventRepository eventRepository)
	{
        _eventRepository = eventRepository;
    }

    public void ScheduleChangeEventStatusToOngoingJob(Event myEvent)
    {
        BackgroundJob.Schedule(() => ChangeEventStatusJob(myEvent.Id.Value, EventStatus.Ongoing), myEvent.StartTime);
    }

    public void ScheduleChangeEventStatusToCompletedJob(Event myEvent)
    {
        BackgroundJob.Schedule(() => ChangeEventStatusJob(myEvent.Id.Value, EventStatus.Completed), myEvent.EndTime);
    }

    [JobDisplayName("ChangeEvent: {0} StatusTo: {1}")]
    public async Task ChangeEventStatusJob(Guid eventId, EventStatus newStatus) {
        var myEvent = await _eventRepository.GetById(eventId);

        if (myEvent is null)
        {
            return;
        }

        myEvent.ChangeEventStatus(newStatus);
        await _eventRepository.SaveChangesAsync();
    }
}

