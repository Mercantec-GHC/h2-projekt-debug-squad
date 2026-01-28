namespace Domain
{
    // Domain entity: Room
    // This class represents a hotel room in our domain.
    // It contains all the business rules and invariants related to a Room.
    public class Room
    {
        // Unique identifier for the room.
        // Using Guid ensures a globally unique ID.
        public Guid Id { get; private set; }

        // Room number (human-readable). 
        // It's a string because it may contain letters, leading zeros, or codes like "A101".
        public string Number { get; private set; } = string.Empty;

        // Maximum number of guests that can stay in the room.
        public int Capacity { get; private set; }

        // Price per night for the room.
        public decimal PricePerNight { get; private set; }

        // Private parameterless constructor for EF Core or serialization.
        // EF Core requires a parameterless constructor to materialize objects from the database.
        private Room() { }

        // Public constructor for creating a new Room in the domain.
        // All business rules are enforced here.
        public Room(string number, int capacity, decimal pricePerNight)
        {
            // Validate inputs before assigning them to ensure the entity is always in a valid state.
            Validate(number, capacity, pricePerNight);

            // Assign validated values
            Number = number;
            Capacity = capacity;
            PricePerNight = pricePerNight;
        }

        // Private static method to validate room data.
        // Static because validation does not depend on any instance state.
        // Ensures the entity's invariants are maintained.
        private static void Validate(string number, int capacity, decimal pricePerNight)
        {
            // Room number is required
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Room number is required");

            // Capacity must be positive
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be greater than 0");

            // Price must be positive
            if (pricePerNight <= 0)
                throw new ArgumentException("Price must be greater than 0");
        }
    }
}
