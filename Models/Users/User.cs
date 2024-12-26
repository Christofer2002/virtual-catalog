namespace VirtualCatalogAPI.Models.Users
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Identification { get; set; }
        public string Password { get; set; } // Hash

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public string RoleName { get; set; }
    }
}
