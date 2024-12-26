namespace VirtualCatalogAPI.Models.Users
{
    public class RolePrivilege
    {
        public long RoleId { get; set; }
        public Role Role { get; set; }

        public long PrivilegeId { get; set; }
        public Privilege Privilege { get; set; }
    }

}
