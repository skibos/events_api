using Events.Domain.Events;

namespace Events.Application.Common.Interfaces.Persistance;

public interface IEventRepository
{
	Task Add(Event newEvent);
}
