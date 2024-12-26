namespace VirtualCatalogAPI.Models.Users
{
    public class Role
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public ICollection<RolePrivilege> RolePrivileges { get; set; } = new List<RolePrivilege>();
    }
}
