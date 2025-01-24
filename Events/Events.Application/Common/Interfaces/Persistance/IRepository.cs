namespace Events.Application.Common.Interfaces.Persistance;

public interface IRepository
{
    Task SaveChangesAsync();
}
