namespace VirtualCatalogAPI.Models.Users
{
    public class Privilege
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<RolePrivilege> RolePrivileges { get; set; } = new List<RolePrivilege>();
    }
}
