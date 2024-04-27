using VideoGameStore.Models;

namespace VideoGameStore.Services
{
    // Interfaz que define los métodos para gestionar los usuarios
    public interface IUserService
    {
        // Obtiene un usuario por su ID 
        Task<User> GetUserByIdAsync(int id);

        // Obtiene todos los usuarios 
        Task<IEnumerable<User>> GetUsersAsync();

        // Registra un usuario con su contraseña
        Task RegisterUserAsync(User user, string password);

        // Actualiza un usuario 
        Task UpdateUserAsync(User user);

        // Elimina un usuario 
        Task DeleteUserAsync(User user);

        // Autentica a un usuario  con su nombre de usuario y contraseña
        Task<bool> AuthenticateUserAsync(string username, string password);

        // Obtiene si el usuario está autenticado  a través del contexto HTTP
        Task<bool?> GetAuthenticatedUserAsync(HttpContext httpContext);

        // Verifica si un usuario es administrador 
        Task<bool> IsUserAdmin(string username);
    }
}
