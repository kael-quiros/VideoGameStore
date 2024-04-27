using VideoGameStore.Data; 
using VideoGameStore.Models; 
using VideoGameStore.Repositories;
using Microsoft.EntityFrameworkCore; 
using BCrypt.Net; 
using VideoGameStore.Services; 

namespace VideoGameStore.Repositories
{
    // Define la clase UserRepository que hereda de Repository<User> e implementa la interfaz IUserRepository
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly VideoGameStoreContext _context; 
        private readonly SessionService _sessionService; 

        // Constructor que inicializa la clase con el contexto de la base de datos
        public UserRepository(VideoGameStoreContext context) : base(context)
        {
            _context = context;
        }

        // Método para obtener un usuario por su ID 
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        // Método para obtener todos los usuarios 
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Método para agregar un usuario 
        public async Task AddUserAsync(User user)
        {
            // Generar el hash de la contraseña
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // Asigna el valor de IsAdmin según sea necesario
            user.IsAdmin = false; // Cambiar a true si deseas crear un administrador por defecto

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        // Método para actualizar un usuario
        public void UpdateUser(User user)
        {
            // Verificar si la contraseña ha sido modificada
            var existingUser = _context.Users.Find(user.UserId);
            if (existingUser.Password != user.Password)
            {
                // Generar un nuevo hash de contraseña
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }

            _context.Users.Update(user);
            _context.SaveChanges();
        }

        // Método para eliminar un usuario
        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        // Método para obtener un usuario por su nombre de usuario y contraseña 
        public async Task<User> GetUserByUsernameAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user != null)
            {
                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
                if (!isPasswordValid)
                {
                    // La contraseña es incorrecta
                    return null;
                }
            }

            return user;
        }

        // Método para eliminar un usuario 
        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        // Método para actualizar un usuario 
        public async Task UpdateUserAsync(User user)
        {
            // Verificar si la contraseña ha sido modificada
            var existingUser = await _context.Users.FindAsync(user.UserId);
            if (existingUser.Password != user.Password)
            {
                // Generar un nuevo hash de contraseña
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        // Método para obtener un usuario por su nombre de usuario 
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user;
        }

        // Método para obtener un usuario por su ID 
        public async Task<User> GetUserByIdAsync(object value)
        {
            if (value is int id)
            {
                return await _context.Users.FindAsync(id);
            }
            else
            {
                return null;
            }
        }

    }
}
