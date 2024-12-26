using VirtualCatalogAPI.Models.Auth;
using VirtualCatalogAPI.Models.Users;

namespace VirtualCatalogAPI.Businesses.Auth
{
    public interface IAuthService
    {
        /// <summary>
        /// Authenticates a user and generates a JWT token.
        /// </summary>
        /// <param name="email">The user's email.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>An authentication response containing user details and a token.</returns>
        Task<AuthResponse> AuthenticateAsync(string email, string password);

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerRequest">The registration request containing user details.</param>
        /// <returns>The registered user.</returns>
        Task<User> RegisterAsync(RegisterRequest registerRequest);

        /// <summary>
        /// Logs out the current user by clearing the authentication token.
        /// </summary>
        void Logout();

        /// <summary>
        /// Checks if the current session is active.
        /// </summary>
        /// <returns>A session status indicating whether the session is active.</returns>
        SessionStatus CheckSession();
    }
}
