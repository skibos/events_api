using Events.Application.Common.Dto;
using Events.Domain.Events;
using Events.Domain.Events.Entities;

namespace Events.Application.Common.Interfaces.Persistance;

public interface IEventRepository : IRepository
{
	Task Add(Event newEvent);
	Task<Event?> GetById(Guid id);
	Task<PaginatedResult<Event>> GetPagedEvents(Guid myUserId, int pageNumber, int pageSize);
	Task<PaginatedResult<Participant>> GetPagedParticipants(Guid eventId, int pageNumber, int pageSize);
}
