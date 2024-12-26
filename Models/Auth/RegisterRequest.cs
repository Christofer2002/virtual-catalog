using System.Diagnostics.CodeAnalysis;

namespace VirtualCatalogAPI.Models.Auth
{
    public class RegisterRequest
    {
        public string Identification { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
    }
}
