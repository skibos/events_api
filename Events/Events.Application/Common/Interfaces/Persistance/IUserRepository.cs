using Events.Domain.Users;

namespace Events.Application.Common.Interfaces.Persistance;

public interface IUserRepository : IRepository
{
	Task Add(User user);
	Task<User?> GetByEmail(string email);
}


