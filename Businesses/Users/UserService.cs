using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VirtualCatalogAPI.Data.Repository.Users;
using VirtualCatalogAPI.Models.Users;

namespace VirtualCatalogAPI.Businesses.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                return await _userRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UserService.GetAllUsersAsync: {ex.Message}");
                throw new Exception("An error occurred while retrieving users.", ex);
            }
        }

        public async Task<User> GetUserByIdAsync(long id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid user ID.", nameof(id));

            try
            {
                return await _userRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("User not found.");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"KeyNotFoundException: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UserService.GetUserByIdAsync: {ex.Message}");
                throw new Exception("An error occurred while retrieving the user.", ex);
            }
        }

        public async Task AddUserAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            try
            {
                if (string.IsNullOrWhiteSpace(user.Name))
                    throw new ArgumentException("User name is required.", nameof(user.Name));

                if (string.IsNullOrWhiteSpace(user.Email))
                    throw new ArgumentException("User email is required.", nameof(user.Email));

                if (string.IsNullOrWhiteSpace(user.Password))
                    throw new ArgumentException("Password is required.", nameof(user.Password));

                user.Password = HashPassword(user.Password);

                await _userRepository.AddAsync(user);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UserService.AddUserAsync: {ex.Message}");
                throw new Exception("An error occurred while adding the user.", ex);
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashedBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            try
            {
                if (string.IsNullOrWhiteSpace(user.Name))
                    throw new ArgumentException("User name is required.", nameof(user.Name));

                if (string.IsNullOrWhiteSpace(user.Email))
                    throw new ArgumentException("User email is required.", nameof(user.Email));

                await _userRepository.UpdateAsync(user);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UserService.UpdateUserAsync: {ex.Message}");
                throw new Exception("An error occurred while updating the user.", ex);
            }
        }

        public async Task DeleteUserAsync(long id)
        {
            if (id <= 0) throw new ArgumentException("Invalid user ID.", nameof(id));

            try
            {
                await _userRepository.DeleteAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"KeyNotFoundException: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UserService.DeleteUserAsync: {ex.Message}");
                throw new Exception("An error occurred while deleting the user.", ex);
            }
        }

        public async Task<User> GetUserByIdentificationAsync(string identification)
        {
            if (string.IsNullOrWhiteSpace(identification))
                throw new ArgumentException("Identification is required.", nameof(identification));

            try
            {
                return await _userRepository.GetByIdentificationAsync(identification) ?? throw new KeyNotFoundException("User not found.");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"KeyNotFoundException: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UserService.GetUserByIdentificationAsync: {ex.Message}");
                throw new Exception("An error occurred while retrieving the user by identification.", ex);
            }
        }
    }
}
