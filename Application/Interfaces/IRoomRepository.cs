using Domain;

namespace Application.Interfaces
{
    // Repository interface for Room entity
    // In DDD, repositories provide an abstraction over data storage,
    // allowing the application layer to work with domain entities
    // without knowing the details of the database or infrastructure.
    public interface IRoomRepository
    {
        // Adds a new Room to the storage.
        // The implementation (e.g., EF Core, in-memory, file, etc.) 
        // is provided in the Infrastructure layer.
        // Application layer only depends on this abstraction.
        Task AddAsync(Room room);
    }
}
