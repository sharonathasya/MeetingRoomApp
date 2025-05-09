using System.ComponentModel.DataAnnotations;

namespace MeetingRoomApp.Data.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? MeetingRoomId { get; set; }
        public DateTime? StartBooking { get; set; }
        public DateTime? EndBooking { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public virtual User User { get; set; }
        public virtual MeetingRoom MeetingRoom { get; set; }
    }
}
