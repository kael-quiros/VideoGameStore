using VideoGameStore.Models;
using VideoGameStore.Repositories;

namespace VideoGameStore.Services
{
    // Implementación de la interfaz IOrderService para gestionar órdenes
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly ShoppingCartRepository _cartRepository;

        // Constructor que recibe los repositorios necesarios para interactuar con los datos
        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
        }

        // Obtiene una orden por su ID 
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetOrderByIdAsync(id);
        }

        // Obtiene todas las órdenes
        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _orderRepository.GetOrdersAsync();
        }

        // Obtiene todas las órdenes de un usuario 
        public async Task<IEnumerable<Order>> GetOrdersByUserAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersAsync();
            return orders.Where(o => o.UserId == userId);
        }

        // Realiza un pedido 
        public async Task PlaceOrderAsync(int userId, ShoppingCart cart)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null || !cart.CartItems.Any())
                return;

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = CalculateTotalAmount(cart.CartItems)
            };

            foreach (var cartItem in cart.CartItems)
            {
                var product = await _productRepository.GetProductByIdAsync(cartItem.ProductId);
                if (product != null)
                {
                    var orderItem = new OrderItem
                    {
                        ProductId = product.ProductId,
                        Quantity = cartItem.Quantity
                    };
                    order.OrderItems.Add(orderItem);
                }
            }

            await _orderRepository.AddOrderAsync(order);
        }

        // Actualiza una orden 
        public async Task UpdateOrderAsync(Order order)
        {
            await _orderRepository.UpdateOrderAsync(order);
        }

        // Cancela una orden 
        public async Task CancelOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order != null)
            {
                // Implementa la lógica para cancelar la orden
                await _orderRepository.DeleteOrderAsync(order);
            }
        }

        // Calcula el monto total de la orden a partir de los elementos del carrito
        private decimal CalculateTotalAmount(IEnumerable<CartItem> cartItems)
        {
            decimal totalAmount = 0;
            foreach (var cartItem in cartItems)
            {
                var product = _productRepository.GetProductByIdAsync(cartItem.ProductId).Result;
                totalAmount += product.Price * cartItem.Quantity;
            }
            return totalAmount;
        }

        // Realiza un pedido para un usuario específico 
        public async Task PlaceOrderAsync(int userId)
        {
            // Obtiene el carrito de compras del usuario
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);

            // Verifica si el carrito existe y tiene elementos
            if (cart != null && cart.CartItems.Any())
            {
                // Crea una nueva orden
                var order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    TotalAmount = CalculateTotalAmount(cart.CartItems)
                };

                // Agrega los elementos del carrito a la orden
                foreach (var cartItem in cart.CartItems)
                {
                    var product = await _productRepository.GetProductByIdAsync(cartItem.ProductId);
                    if (product != null)
                    {
                        var orderItem = new OrderItem
                        {
                            ProductId = product.ProductId,
                            Quantity = cartItem.Quantity
                        };

                        order.OrderItems.Add(orderItem);
                    }
                }

                // Guarda la orden
                await _orderRepository.AddOrderAsync(order);

                // Limpia el carrito de compras
                cart.CartItems.Clear();
                await _cartRepository.UpdateCartAsync(cart);
            }
        }
    }
}
