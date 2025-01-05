using System;
using Events.Application.Common.Dto;
using Events.Application.Common.Interfaces.Persistance;
using Events.Domain.Events;
using Events.Domain.Events.Entities;
using Events.Domain.Users.ValueObjects;
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

        public async Task<PaginatedResult<Event>> GetPagedEvents(Guid myUserId, int pageNumber, int pageSize)
        {
            var totalResults = await _dbContext.Events
            .Where(e => e.Participants.Any(p => p.UserId == myUserId))
            .CountAsync();

            var totalPages = (int)Math.Ceiling(totalResults / (double)pageSize);

            var events = await _dbContext.Events
                .Where(e => e.Participants.Any(p => p.UserId == myUserId))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<Event>(events, !events.Any(), pageNumber, pageSize, totalPages, totalResults);
        }

        public async Task<PaginatedResult<Participant>> GetPagedParticipants(Guid eventId, int pageNumber, int pageSize)
        {
            var totalResults = await _dbContext.Events
                .Where(e => e.Id == eventId)
                .SelectMany(e => e.Participants)
                .AsNoTracking()
                .CountAsync();

            var totalPages = (int)Math.Ceiling(totalResults / (double)pageSize);

            var participants = await _dbContext.Events
                .Include(e => e.Participants)
                .Where(e => e.Id == eventId)
                .SelectMany(e => e.Participants)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return new PaginatedResult<Participant>(participants, !participants.Any(), pageNumber, pageSize, totalPages, totalResults);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

