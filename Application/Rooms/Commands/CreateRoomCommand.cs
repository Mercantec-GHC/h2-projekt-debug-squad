namespace Application.Rooms.Commands
{
    // Command / DTO for creating a new Room
    // In Clean Architecture / DDD, commands are simple data carriers
    // that represent a single use case or action in the application.
    // They contain only the information needed to perform the operation,
    // and they do not contain business logic themselves.
    public class CreateRoomCommand
    {
        // Room number (human-readable)
        // Example: "101", "A12", "Penthouse"
        public string Number { get; init; } = string.Empty;

        // Maximum number of guests that can stay in this room
        public int Capacity { get; init; }

        // Price per night for this room
        public decimal PricePerNight { get; init; }
    }
}
