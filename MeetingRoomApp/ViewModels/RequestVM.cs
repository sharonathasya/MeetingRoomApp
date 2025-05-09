namespace BookingService.ViewModels
{

    public class ReqBooking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MeetingRoomId { get; set; }
        public DateTime StartBooking { get; set; }
        public DateTime EndBooking { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        //public DateTime CreatedAt { get; set; }
    }

    public class ReqIdBooking
    {
        public string? Id { get; set; }
    }
}
