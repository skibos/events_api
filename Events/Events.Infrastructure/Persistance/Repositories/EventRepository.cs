using System;
using Events.Application.Common.Interfaces.Persistance;
using Events.Domain.Events;
using Events.Domain.Events.Entities;
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

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

