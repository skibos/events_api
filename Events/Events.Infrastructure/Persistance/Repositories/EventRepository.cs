using System;
using Events.Application.Common.Dto;
using Events.Application.Common.Interfaces.Persistance;
using Events.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.Persistance.Repositories
{
	public class EventRepository : IEventRepository
	{
        private readonly AppDbContext _dbContext;

		public EventRepository(AppDbContext dbContext)
		{
            _dbContext = dbContext;
		}

        public async Task Add(Event newEvent)
        {
            _dbContext.Add(newEvent);
            await SaveChangesAsync();
        }

        public async Task<Event?> GetById(Guid id)
        {
            return await _dbContext.Events.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<PaginatedResult<Event>> GetPagedEvents(Guid myId, int pageNumber, int pageSize)
        {
            var totalResults = await _dbContext.Events
            .Where(e => e.Participants.Any(p => p.UserId == myId))
            .CountAsync();

            var totalPages = (int)Math.Ceiling(totalResults / (double)pageSize);

            var events = await _dbContext.Events
                .Where(e => e.Participants.Any(p => p.UserId == myId))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<Event>(events, !events.Any(), pageNumber, pageSize, totalPages, totalResults);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

