using System.ComponentModel.DataAnnotations;

namespace MeetingRoomApp.Data.Models
{
    public class TblMasterRole
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public AccountRole AccountRole { get; set; }
    }
}
