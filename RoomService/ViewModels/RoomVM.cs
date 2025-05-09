namespace RoomService.ViewModels
{
    public class RoomRes
    {
        public string? STATUS { get; set; }
        public string? MESSAGE { get; set; }
        public DataRoom? RESULT { get; set; }
    }

    public class RoomResList
    {
        public string? STATUS { get; set; }
        public string? MESSAGE { get; set; }
        public List<DataRoom>? RESULT { get; set; }
    }

    public class DataRoom
    {
        public int? Id { get; set; }
        public string? RoomName { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
