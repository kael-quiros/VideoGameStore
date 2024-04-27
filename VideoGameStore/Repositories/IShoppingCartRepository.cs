using VideoGameStore.Models; 

namespace VideoGameStore.Repositories
{
    // Define una interfaz IShoppingCartRepository que hereda de IRepository<ShoppingCart>
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        // Método para agregar un carrito de compras de manera asíncrona
        Task AddCartAsync(ShoppingCart cart);

        // Método para obtener un carrito por su ID de manera asíncrona
        Task<ShoppingCart> GetCartByIdAsync(int id);

        // Método para obtener un carrito por el ID de usuario de manera asíncrona
        Task<ShoppingCart> GetCartByUserIdAsync(int userId);

        // Método para obtener todos los carritos de manera asíncrona
        Task<IEnumerable<ShoppingCart>> GetCartsAsync();

        // Método para obtener todos los carritos con sus elementos de manera asíncrona
        Task<IEnumerable<ShoppingCart>> GetCartsWithItemsAsync();

        // Método para actualizar un carrito de compras de manera asíncrona
        Task UpdateCartAsync(ShoppingCart cart);
    }
}
