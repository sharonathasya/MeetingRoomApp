using System.ComponentModel.DataAnnotations;

namespace MeetingRoomApp.Data.Models
{
    public class MeetingRoom
    {
        [Key]
        public int Id { get; set; }
        public string? RoomName { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
