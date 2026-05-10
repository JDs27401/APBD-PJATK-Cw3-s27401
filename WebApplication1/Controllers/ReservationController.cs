using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : Controller
{
    [HttpGet]
    public IActionResult Get([FromQuery] DateTime? date, [FromQuery] string? status, [FromQuery] int? roomId)
    {
        var query = TestData.TestData.Reservations.AsQueryable();
        if (date.HasValue)
        {
            query = query.Where(r => r.Date == date.Value.Date);
        }

        if (!string.IsNullOrEmpty(status))
        {
            query = query.Where(r => r.Status == status);
        }

        if (roomId.HasValue)
        {
            query = query.Where(r => r.RoomId == roomId.Value);
        }
        
        return Ok(query);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var query = TestData.TestData.Rooms.FirstOrDefault(r => r.Id == id);
        if (query == null)
        {
            return NotFound();
        }
        return Ok(query);
    }

    [HttpPost]
    public IActionResult Post(Reservation reservation)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (reservation.EndTime <= reservation.StartTime)
        {
            return BadRequest("EndTime must be greater than StartTime");    
        }
        

        var room = TestData.TestData.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
        if (room == null)
        {
            return BadRequest("Room does not exist");
        }

        if (!room.IsActive)
        {
            return BadRequest("Room is inactive");
        }
        
        var conflict = TestData.TestData.Reservations.Any(r =>
            r.RoomId == reservation.RoomId &&
            r.Date.Date == reservation.Date.Date &&
            reservation.StartTime < r.EndTime &&
            reservation.EndTime > r.StartTime);

        if (conflict)
        {
            return Conflict("Time conflict");
        }

        reservation.Id = TestData.TestData.Reservations.Max(r => r.Id) + 1;
        TestData.TestData.Reservations.Add(reservation);

        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
    }
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, Reservation updated)
    {
        var res = TestData.TestData.Reservations.FirstOrDefault(r => r.Id == id);
        if (res == null)
        {
            return NotFound();
        }

        res.RoomId = updated.RoomId;
        res.OrganizerName = updated.OrganizerName;
        res.Topic = updated.Topic;
        res.Date = updated.Date;
        res.StartTime = updated.StartTime;
        res.EndTime = updated.EndTime;
        res.Status = updated.Status;

        return Ok(res);
    }
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var res = TestData.TestData.Reservations.FirstOrDefault(r => r.Id == id);
        if (res == null)
        {
            return NotFound();
        }

        TestData.TestData.Reservations.Remove(res);
        return NoContent();
    }
}