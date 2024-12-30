﻿using Events.Application.Common.Interfaces.Persistance;
using Events.Domain.Users;

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
            await _dbContext.SaveChangesAsync();
        }
    }
}

