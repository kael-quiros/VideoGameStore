using VideoGameStore.Models;
using VideoGameStore.Repositories;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;

namespace VideoGameStore.Services
{
    // Implementación de la interfaz IUserService para gestionar las operaciones relacionadas con los usuarios
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISessionService _sessionService;

        // Constructor que recibe el repositorio de usuarios y el servicio de sesión
        public UserService(IUserRepository userRepository, ISessionService sessionService)
        {
            _userRepository = userRepository;
            _sessionService = sessionService;
        }

        // Obtiene un usuario por su ID
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        // Obtiene todos los usuarios
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userRepository.GetUsersAsync();
        }

        // Registra un nuevo usuario con la contraseña proporcionada
        public async Task RegisterUserAsync(User user, string password)
        {
            user.Password = password; // Almacena la contraseña 
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password); // Generar el hash de la contraseña
            await _userRepository.AddUserAsync(user);
        }

        // Actualiza la información de un usuario
        public async Task UpdateUserAsync(User user)
        {
            // Verifica si la contraseña ha sido modificada
            var existingUser = await _userRepository.GetUserByIdAsync(user.UserId);
            if (existingUser.Password != user.Password)
            {
                // Genera un nuevo hash de contraseña
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }

            await _userRepository.UpdateUserAsync(user);
        }

        // Elimina un usuario
        public async Task DeleteUserAsync(User user)
        {
            await _userRepository.DeleteUserAsync(user);
        }

        // Autentica un usuario utilizando el nombre de usuario y la contraseña proporcionados
        public async Task<bool> AuthenticateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username, password);
            if (user != null)
            {
                _sessionService.SetUserId(user.UserId);
                return true;
            }

            return false;
        }

        // Verifica si un usuario es administrador
        public bool IsUserAdmin(User user)
        {
            return (bool)user.IsAdmin;
        }

        // Obtiene el estado de autenticación del usuario a partir del contexto de HTTP
        public async Task<bool?> GetAuthenticatedUserAsync(HttpContext httpContext)
        {
            // Obtiene el nombre de usuario del contexto de HTTP
            string username = httpContext.Session.GetString("Username");

            // Obtiene el usuario desde el repositorio
            var user = await _userRepository.GetUserByUsernameAsync(username);

            // Verifica si el usuario existe y es administrador
            if (user != null)
            {
                return IsUserAdmin(user);
            }
            else
            {
                // Maneja el caso en que no se pueda encontrar el usuario
                return null;
            }
        }

        // Verifica si un usuario es administrador a partir del nombre de usuario
        public async Task<bool> IsUserAdmin(string username)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user != null)
            {
                return IsUserAdmin(user);
            }
            else
            {
                return false;
            }
        }
    }
}
