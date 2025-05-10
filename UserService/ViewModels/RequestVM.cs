namespace UserService.ViewModels
{
    public class ReqLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class ReqAddUser
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool? IsActive { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public bool? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public int? RoleId { get; set; }

    }
    public class ReqIdUser
    {
        public string id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
