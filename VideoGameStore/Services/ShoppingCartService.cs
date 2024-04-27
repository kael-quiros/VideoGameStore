using VideoGameStore.Models;
using VideoGameStore.Repositories;

namespace VideoGameStore.Services
{
    // Implementación de la interfaz IShoppingCartService para gestionar el carrito de compras
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _cartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderService _orderService;

        // Constructor que recibe los repositorios necesarios para operaciones relacionadas con el carrito de compras
        public ShoppingCartService(IShoppingCartRepository cartRepository, IUserRepository userRepository, IProductRepository productRepository, IOrderService orderService)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _orderService = orderService;
        }

        // Obtiene un carrito de compras por su ID
        public async Task<ShoppingCart> GetCartByIdAsync(int id)
        {
            return await _cartRepository.GetCartByIdAsync(id);
        }

        // Obtiene todos los carritos de compras
        public async Task<IEnumerable<ShoppingCart>> GetCartsAsync()
        {
            return await _cartRepository.GetCartsAsync();
        }

        // Agrega un producto al carrito de compras
        public async Task AddToCartAsync(int userId, int productId, int quantity)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            var product = await _productRepository.GetProductByIdAsync(productId);

            if (user == null || product == null)
                return;

            var cart = await _cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                cart = new ShoppingCart { UserId = userId };
                await _cartRepository.AddCartAsync(cart);
            }

            var cartItem = new CartItem { ProductId = productId, Quantity = quantity };
            cart.CartItems.Add(cartItem);
            await _cartRepository.UpdateCartAsync(cart);
        }

        // Elimina un producto del carrito de compras
        public async Task RemoveFromCartAsync(int userId, int productId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);

            if (cart != null)
            {
                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

                if (cartItem != null)
                {
                    cart.CartItems.Remove(cartItem);
                    await _cartRepository.UpdateCartAsync(cart);
                }
            }
        }

        // Actualiza el carrito de compras
        public async Task UpdateCartAsync(ShoppingCart cart)
        {
            await _cartRepository.UpdateCartAsync(cart);
        }

        // Procesa el checkout del carrito de compras, creando una nueva orden
        public async Task CheckoutAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);

            if (cart != null && cart.CartItems.Any())
            {
                await _orderService.PlaceOrderAsync(userId, cart);
            }
        }

        // Obtiene el carrito de compras de un usuario por su ID de usuario
        public async Task<ShoppingCart> GetCartByUserIdAsync(int userId)
        {
            return await _cartRepository.GetCartByUserIdAsync(userId);
        }
    }
}
