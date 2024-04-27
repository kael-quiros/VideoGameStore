using VideoGameStore.Models;

namespace VideoGameStore.Services
{
    // Interfaz que define los métodos para gestionar el carrito de compras
    public interface IShoppingCartService
    {
        // Obtiene el carrito por su ID 
        Task<ShoppingCart> GetCartByIdAsync(int id);

        // Obtiene todos los carritos 
        Task<IEnumerable<ShoppingCart>> GetCartsAsync();

        // Agrega un producto al carrito
        Task AddToCartAsync(int userId, int productId, int quantity);

        // Elimina un producto del carrito 
        Task RemoveFromCartAsync(int userId, int productId);

        // Actualiza el carrito de compras a
        Task UpdateCartAsync(ShoppingCart cart);

        // Realiza el proceso de checkout del carrito de compras 
        Task CheckoutAsync(int userId);

        // Obtiene el carrito de compras por el ID del usuario 
        Task<ShoppingCart> GetCartByUserIdAsync(int userId);
    }
}
