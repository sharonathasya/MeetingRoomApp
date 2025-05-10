using System.ComponentModel.DataAnnotations;

namespace MeetingRoomApp.Data.Models
{
    public class AccountRole
    {
        [Key]
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public int? RoleId { get; set; }
        public Account Account { get; set; }
        public TblMasterRole TblMasterRole { get; set; }
    }
}
