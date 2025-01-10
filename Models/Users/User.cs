namespace VirtualCatalogAPI.Models.Users
{
    public class User
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Identification { get; set; }
        public required string Password { get; set; } // Hash

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public string RoleName { get; set; }
    }
}
