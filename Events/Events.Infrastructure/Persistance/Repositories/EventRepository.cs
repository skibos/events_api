using System;
using Events.Application.Common.Interfaces.Persistance;
using Events.Domain.Events;

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
            await _dbContext.SaveChangesAsync();
        }
    }
}

