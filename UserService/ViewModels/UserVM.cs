namespace UserService.ViewModels
{
    public class UserRes
    {
        public string? STATUS { get; set; }
        public string? MESSAGE { get; set; }
        public DataUser? RESULT { get; set; }
    }

    public class DataUser
    {
        public int? Userid { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime? CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public DateTime? DeletedTime { get; set; }
        public bool? IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
