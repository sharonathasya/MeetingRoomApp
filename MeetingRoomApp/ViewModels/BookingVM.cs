namespace BookingService.ViewModels
{
    public class BookingRes
    {
        public string? STATUS { get; set; }
        public string? MESSAGE { get; set; }
        public DataBooking? RESULT { get; set; }
    }

    public class BookingResList
    {
        public string? STATUS { get; set; }
        public string? MESSAGE { get; set; }
        public List<DataBooking>? RESULT { get; set; }
    }

    public class DataBooking
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int? MeetingRoomId { get; set; }
        public DateTime? StartBooking { get; set; }
        public DateTime? EndBooking { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
