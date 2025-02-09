﻿using Events.Application.Common.Interfaces.Persistance;
using Events.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Events.Infrastructure.Persistance.Repositories
{
	public class UserRepository : IUserRepository
	{
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
		{
            _dbContext = dbContext;
		}

        public async Task Add(User user)
        {
            _dbContext.Add(user);
            await SaveChangesAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetById(Guid id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetUsersByIds(List<Guid> ids)
        {
            return await _dbContext.Users.Where(u => ids.Contains(u.Id)).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}

