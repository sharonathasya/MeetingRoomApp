namespace RoomService.ViewModels
{

    public class ReqRoom
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ReqIdRoom
    {
        public string? Id { get; set; }
        public string? RoomName { get; set; }
        public string? Description { get; set; }
    }
}
