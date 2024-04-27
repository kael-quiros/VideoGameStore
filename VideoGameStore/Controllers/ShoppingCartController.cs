using Microsoft.AspNetCore.Mvc; 
using VideoGameStore.Models; 
using VideoGameStore.Services; 

namespace VideoGameStore.Controllers 
{
    public class ShoppingCartController : Controller 
    {
        private readonly IShoppingCartService _shoppingCartService; // Declara una variable para almacenar una instancia del servicio de carrito de compras
        private readonly ISessionService _sessionService; // Declara una variable para almacenar una instancia del servicio de sesión

        public ShoppingCartController(IShoppingCartService shoppingCartService, ISessionService sessionService)
        {
            _shoppingCartService = shoppingCartService; // Inicializa la variable del servicio de carrito de compras con la instancia proporcionada
            _sessionService = sessionService; // Inicializa la variable del servicio de sesión con la instancia proporcionada
        }

        //  Muestra el carrito de compras del usuario actual
        public async Task<IActionResult> Index()
        {
            int userId = GetUserId(); // Obtiene el ID del usuario actual
            var cart = await _shoppingCartService.GetCartByUserIdAsync(userId); // Obtiene el carrito de compras del usuario
            return View(cart); // Devuelve la vista con el carrito de compras
        }

        // Agrega un producto al carrito de compras
        public async Task<IActionResult> AddToCart(int id, int quantity = 1)
        {
            int userId = GetUserId(); // Obtiene el ID del usuario actual
            await _shoppingCartService.AddToCartAsync(userId, id, quantity); // Agrega el producto al carrito de compras
            return RedirectToAction("Index", "Home"); // Redirige a la página de inicio después de agregar el producto
        }

        // Elimina un producto del carrito de compras
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            int userId = GetUserId(); // Obtiene el ID del usuario 
            await _shoppingCartService.RemoveFromCartAsync(userId, id); // Elimina el producto del carrito de compras
            return RedirectToAction("Index"); // Redirige de vuelta al carrito de compras
        }

        // Actualiza la cantidad de un producto en el carrito de compras
        public async Task<IActionResult> UpdateCartItem(int id, int quantity)
        {
            int userId = GetUserId(); // Obtiene el ID del usuario actual
            var cart = await _shoppingCartService.GetCartByUserIdAsync(userId); // Obtiene el carrito de compras del usuario
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == id); // Encuentra el ítem del carrito con el ID del producto
            if (cartItem != null)
            {
                cartItem.Quantity = quantity; // Actualiza la cantidad del ítem del carrito
                await _shoppingCartService.UpdateCartAsync(cart); // Actualiza el carrito de compras 
            }
            return RedirectToAction("Index"); // Redirige de vuelta al carrito de compras
        }

        //  Procesa el pago y finaliza la compra
        public async Task<IActionResult> Checkout()
        {
            int userId = GetUserId(); // Obtiene el ID del usuario actual
            await _shoppingCartService.CheckoutAsync(userId); // Procesa el pago y finaliza la compra 
            return RedirectToAction("Index", "Orders"); // Redirige a la página de pedidos después del proceso de pago
        }

        //Obtiene el ID del usuario actual desde el servicio de sesión
        private int GetUserId()
        {
            return _sessionService.GetUserId(); 
        }
    }
}
