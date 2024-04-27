using VideoGameStore.Models; 
using Microsoft.EntityFrameworkCore; 
using VideoGameStore.Data; 

namespace VideoGameStore.Repositories
{
    // Define la clase ShoppingCartRepository que hereda de Repository<ShoppingCart>
   
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private readonly VideoGameStoreContext _context; // Declara una instancia del contexto de la base de datos

        // Constructor que inicializa la clase de la base de datos
        public ShoppingCartRepository(VideoGameStoreContext context) : base(context)
        {
            _context = context;
        }

        // Método para obtener un carrito por su ID
        public async Task<ShoppingCart> GetCartByIdAsync(int id)
        {
            return await _context.ShoppingCarts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.CartId == id);
        }

        // Método para obtener todos los carritos 
        public async Task<IEnumerable<ShoppingCart>> GetCartsAsync()
        {
            return await _context.ShoppingCarts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ToListAsync();
        }

        // Método para agregar un carrito 
        public async Task AddCartAsync(ShoppingCart cart)
        {
            _context.ShoppingCarts.Add(cart);
            await _context.SaveChangesAsync();
        }

        // Método para actualizar un carrito 
        public async Task UpdateCartAsync(ShoppingCart cart)
        {
            _context.ShoppingCarts.Update(cart);
            await _context.SaveChangesAsync();
        }

        // Método para eliminar un carrito 
        public async Task DeleteCartAsync(ShoppingCart cart)
        {
            _context.ShoppingCarts.Remove(cart);
            await _context.SaveChangesAsync();
        }

        // Método para obtener un carrito por el ID del usuario 
        public async Task<ShoppingCart> GetCartByUserIdAsync(int userId)
        {
            return await _context.ShoppingCarts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        // Método para obtener todos los carritos con sus elementos
        public async Task<IEnumerable<ShoppingCart>> GetCartsWithItemsAsync()
        {
            return await _context.ShoppingCarts
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .ToListAsync();
        }
    }
}
