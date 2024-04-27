using VideoGameStore.Models; 

namespace VideoGameStore.Repositories
{
    // Define una interfaz IUserRepository que hereda de IRepository<User>
    public interface IUserRepository : IRepository<User>
    {
        // Método para agregar un usuario de manera asíncrona
        Task AddUserAsync(User user);

        // Método para eliminar un usuario de manera asíncrona
        Task DeleteUserAsync(User user);

        // Método para obtener un usuario por su ID de manera asíncrona
        Task<User> GetUserByIdAsync(int userId);

        // Método para obtener un usuario por un valor de identificación de manera asíncrona
        Task<User> GetUserByIdAsync(object value);

        // Método para obtener un usuario por su nombre de usuario y contraseña de manera asíncrona
        Task<User> GetUserByUsernameAsync(string username, string password);

        // Método para obtener un usuario por su nombre de usuario de manera asíncrona
        Task<User> GetUserByUsernameAsync(string? username);

        // Método para obtener todos los usuarios de manera asíncrona
        Task<IEnumerable<User>> GetUsersAsync();

        // Método para actualizar un usuario de manera asíncrona
        Task UpdateUserAsync(User user);
    }
}
