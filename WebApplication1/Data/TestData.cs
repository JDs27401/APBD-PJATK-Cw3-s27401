using WebApplication1.Models;

namespace WebApplication1.TestData;

public static class TestData
{
    public static List<Room> Rooms = new List<Room>
    {
        new Room { Id = 1, Name = "Room A1", BuildingCode = "A", Floor = 1, Capacity = 20, HasProjector = true, IsActive = true },
        new Room { Id = 2, Name = "Room B1", BuildingCode = "B", Floor = 2, Capacity = 30, HasProjector = true, IsActive = true },
        new Room { Id = 3, Name = "Room C1", BuildingCode = "C", Floor = 3, Capacity = 15, HasProjector = false, IsActive = true },
        new Room { Id = 4, Name = "Room D1", BuildingCode = "D", Floor = 1, Capacity = 10, HasProjector = false, IsActive = false }
    };

    public static List<Reservation> Reservations = new List<Reservation>
    {
        new Reservation { Id = 1, RoomId = 1, OrganizerName = "Jan Kowalski", Topic = "C#", Date = DateTime.Today, StartTime = new TimeSpan(10,0,0), EndTime = new TimeSpan(12,0,0), Status = "confirmed" },
        new Reservation { Id = 2, RoomId = 2, OrganizerName = "Anna Nowak", Topic = "REST API", Date = DateTime.Today, StartTime = new TimeSpan(13,0,0), EndTime = new TimeSpan(15,0,0), Status = "planned" }
    };
}