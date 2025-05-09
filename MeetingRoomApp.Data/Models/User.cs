using System.ComponentModel.DataAnnotations;

namespace MeetingRoomApp.Data.Models
{
    public class User
    {
        [Key]
        public int? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Address { get; set; }
        public DateTime? CreatedTime { get; set; }
       // public int? AccountId { get; set; }
        public virtual Account Account { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
