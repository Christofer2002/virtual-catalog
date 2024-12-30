using System.Text;
using VirtualCatalogAPI.Models.Users;
using VirtualCatalogAPI.Models.Auth;
using VirtualCatalogAPI.Data.Repository.Auth;
using VirtualCatalogAPI.Helpers;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;

namespace VirtualCatalogAPI.Businesses.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtHelper _jwtHelper;

        public AuthService(IAuthRepository authRepository, IHttpContextAccessor httpContextAccessor, JwtHelper jwtHelper)
        {
            _authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _jwtHelper = jwtHelper ?? throw new ArgumentNullException(nameof(jwtHelper));
        }

        public async Task<AuthResponse> AuthenticateAsync(string email, string password)
        {
            var user = await _authRepository.GetUserByEmailAsync(email);

            if (user == null || !VerifyPassword(password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var token = _jwtHelper.GenerateToken(user.Id.ToString(), user.RoleName, TimeSpan.FromMinutes(60));

            return new AuthResponse
            {
                UserId = user.Id,
                Email = user.Email,
                UserName = user.Name + " " + user.LastName,
                Role = user.RoleName,
                Token = token
            };
        }

        public async Task<User> RegisterAsync(RegisterRequest registerRequest)
        {
            if (await _authRepository.GetUserByEmailAsync(registerRequest.Email) != null)
            {
                throw new InvalidOperationException("Email is already in use.");
            }

            var hashedPassword = ComputeSha256Hash(registerRequest.Password);

            var user = new User
            {
                Identification = registerRequest.Identification,
                Name = registerRequest.Name,
                LastName = registerRequest.LastName,
                Email = registerRequest.Email,
                Password = hashedPassword,
                UserRoles = new List<UserRole>
                {
                    new UserRole
                    {
                        RoleId = 2 // Assuming RoleId 2 is for regular users
                    }
                }
            };

            await _authRepository.CreateUserAsync(user, registerRequest.RoleId);
            return user;
        }

        public void Logout()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context != null)
            {
                context.Response.Cookies.Delete("AuthToken");
            }
        }

        public SessionStatus CheckSession()
        {
            var context = _httpContextAccessor.HttpContext;
            var token = context?.Request.Cookies["AuthToken"];

            if (string.IsNullOrEmpty(token))
            {
                return new SessionStatus(false, "Invalid");
            }

            // Additional token validation can be implemented here if needed.

            return new SessionStatus(true, "Active");
        }

        private bool VerifyPassword(string inputPassword, string storedPasswordHash)
        {
            // Hash the input password and compare it with the stored hash.
            var hashedInputPassword = ComputeSha256Hash(inputPassword);
            return hashedInputPassword == storedPasswordHash;
        }

        private static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public async Task<AuthResponse> RequestPasswordReset(string email)
        {
            var user = await _authRepository.GetUserByEmailAsync(email);

            var token = _jwtHelper.GenerateToken(user.Id.ToString(), user.RoleName, TimeSpan.FromMinutes(60));

            return new AuthResponse
            {
                UserId = user.Id,
                Email = user.Email,
                UserName = user.Name + " " + user.LastName,
                Role = user.RoleName,
                Token = token
            };
        }
    }
}
