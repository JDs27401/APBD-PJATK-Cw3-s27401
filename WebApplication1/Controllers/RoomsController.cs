using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.TestData;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll([FromQuery] int? minCapacity, [FromQuery] bool? hasProjector, [FromQuery] bool? activeOnly)
    {
        var rooms = TestData.TestData.Rooms.AsQueryable();

        if (minCapacity.HasValue)
            rooms = rooms.Where(r => r.Capacity >= minCapacity);

        if (hasProjector.HasValue)
            rooms = rooms.Where(r => r.HasProjector == hasProjector);

        if (activeOnly == true)
            rooms = rooms.Where(r => r.IsActive);

        return Ok(rooms.ToList());
    }

    // GET /api/rooms/{id}
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var room = TestData.TestData.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null) return NotFound();

        return Ok(room);
    }

    // GET /api/rooms/building/{buildingCode}
    [HttpGet("building/{buildingCode}")]
    public IActionResult GetByBuilding(string buildingCode)
    {
        var rooms = TestData.TestData.Rooms
            .Where(r => r.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(rooms);
    }

    // POST
    [HttpPost]
    public IActionResult Create(Room room)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        room.Id = TestData.TestData.Rooms.Max(r => r.Id) + 1;
        TestData.TestData.Rooms.Add(room);

        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }

    // PUT
    [HttpPut("{id}")]
    public IActionResult Update(int id, Room updated)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var room = TestData.TestData.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null) return NotFound();

        room.Name = updated.Name;
        room.BuildingCode = updated.BuildingCode;
        room.Floor = updated.Floor;
        room.Capacity = updated.Capacity;
        room.HasProjector = updated.HasProjector;
        room.IsActive = updated.IsActive;

        return Ok(room);
    }

    // DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var room = TestData.TestData.Rooms.FirstOrDefault(r => r.Id == id);
        if (room == null) return NotFound();

        if (TestData.TestData.Reservations.Any(r => r.RoomId == id))
            return Conflict("Room has reservations");

        TestData.TestData.Rooms.Remove(room);
        return NoContent();
    }
}