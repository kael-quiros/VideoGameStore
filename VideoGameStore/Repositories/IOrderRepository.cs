using VideoGameStore.Models; // Importa los nombres que contiene la clase Order

namespace VideoGameStore.Repositories
{
    // Define una interfaz llamada IOrderRepository que hereda de IRepository<Order>
    public interface IOrderRepository : IRepository<Order>
    {
        //  obtiene todas las órdenes con sus items (productos)
        Task<IEnumerable<Order>> GetOrdersWithItemsAsync();

        // obtiene todas las órdenes asociadas a un usuario mediante su ID
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);

        // obtiene una orden específica por su ID
        Task<Order> GetOrderByIdAsync(int id);

        // obtiene todas las órdenes existentes
        Task<IEnumerable<Order>> GetOrdersAsync();

        // obtiene una nueva orden
        Task AddOrderAsync(Order order);

        // Método para actualizar una orden existente
        Task UpdateOrderAsync(Order order);

        // Método para eliminar una orden existente
        Task DeleteOrderAsync(Order order);
    }
}
