using System.ComponentModel.DataAnnotations;

namespace MeetingRoomApp.Data.Models
{
    public class Account
    {
        [Key]
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [Display(Name = "Password")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()-+]).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*()-+)")]
        public string? Password { get; set; }

        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public bool? IsActive { get; set; }
        public virtual User User { get; set; }
        public ICollection<AccountRole> AccountRoles { get; set; }
    }
}
