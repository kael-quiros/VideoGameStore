using VideoGameStore.Models;

namespace VideoGameStore.Services
{
    // Interfaz que define los métodos  de órdenes
    public interface IOrderService
    {
        // Obtiene una orden por su ID
        Task<Order> GetOrderByIdAsync(int id);

        // Obtiene todas las órdenes disponibles 
        Task<IEnumerable<Order>> GetOrdersAsync();

        // Obtiene todas las órdenes asociadas a un usuario 
        Task<IEnumerable<Order>> GetOrdersByUserAsync(int userId);

        // Coloca una nueva orden para un usuario  utilizando su carrito de compras
        Task PlaceOrderAsync(int userId, ShoppingCart cart);

        // Actualiza una orden existente
        Task UpdateOrderAsync(Order order);

        // Cancela una orden por su ID 
        Task CancelOrderAsync(int orderId);

        // Coloca una nueva orden de forma para un usuario 
        Task PlaceOrderAsync(int userId);
    }
}
