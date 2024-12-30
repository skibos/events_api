using Events.Domain.Users;

namespace Events.Application.Common.Interfaces.Persistance
{
	public interface IUserRepository
	{
		Task Add(User user);
	}
}

