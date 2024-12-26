namespace VirtualCatalogAPI.Models.Auth
{
    public class AuthResponse
    {
        public long UserId { get; set; }
        public required string Email { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
