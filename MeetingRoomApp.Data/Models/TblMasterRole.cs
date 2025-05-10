using System.ComponentModel.DataAnnotations;

namespace MeetingRoomApp.Data.Models
{
    public class TblMasterRole
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public ICollection<AccountRole> AccountRoles { get; set; } = new List<AccountRole>();
    }
}
