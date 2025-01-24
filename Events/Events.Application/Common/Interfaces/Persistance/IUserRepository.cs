using Events.Domain.Users;

namespace Events.Application.Common.Interfaces.Persistance;

public interface IUserRepository : IRepository
{
	Task Add(User user);
	Task<User?> GetByEmail(string email);
	Task<User?> GetById(Guid id);
	Task<List<User>> GetUsersByIds(List<Guid> ids);
}


