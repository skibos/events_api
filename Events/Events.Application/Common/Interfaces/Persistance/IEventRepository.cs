using Events.Application.Common.Dto;
using Events.Domain.Events;

namespace Events.Application.Common.Interfaces.Persistance;

public interface IEventRepository : IRepository
{
	Task Add(Event newEvent);
	Task<Event?> GetById(Guid id);
	Task<PaginatedResult<Event>> GetPagedEvents(Guid myId, int pageNumber, int pageSize);
}
