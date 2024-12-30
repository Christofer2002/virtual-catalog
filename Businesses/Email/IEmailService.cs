namespace VirtualCatalogAPI.Businesses.Email
{
    public interface IEmailService
    {
        Task SendPasswordResetEmail(string email, string token);
    }
}
