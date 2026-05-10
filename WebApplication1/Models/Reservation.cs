using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Reservation
{
    public int Id { get; set; }
    
    [Required]
    public int RoomId { get; set; }
    
    [Required]
    public string OrganizerName { get; set; }
    
    [Required]
    public string Topic { get; set; }
    
    public DateTime Date { get; set; }
    
    public TimeSpan StartTime { get; set; }
    
    public TimeSpan EndTime { get; set; }
    
    public string Status { get; set; }
}