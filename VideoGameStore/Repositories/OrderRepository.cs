using VideoGameStore.Models; 
using Microsoft.EntityFrameworkCore; 
using VideoGameStore.Data; 

namespace VideoGameStore.Repositories
{
    // Define la clase OrderRepository que implementa la interfaz IOrderRepository y hereda de Repository<Order>
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly VideoGameStoreContext _context; 

        // Constructor que inicializa la clase de la base de datos
        public OrderRepository(VideoGameStoreContext context) : base(context)
        {
            _context = context;
        }

        // Método para obtener una orden por su ID de manera asíncrona
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        // Método para obtener todas las órdenes de manera asíncrona
        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ToListAsync();
        }

        // Método para agregar una orden de manera asíncrona
        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        // Método para actualizar una orden de manera asíncrona
        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        // Método para eliminar una orden de manera asíncrona
        public async Task DeleteOrderAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        // Método para obtener todas las órdenes con sus elementos de pedido de manera asíncrona
        public async Task<IEnumerable<Order>> GetOrdersWithItemsAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        // Método para obtener todas las órdenes de un usuario específico de manera asíncrona
        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }
    }
}
