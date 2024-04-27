using Microsoft.AspNetCore.Mvc; 
using VideoGameStore.Models; 
using VideoGameStore.Services; 

namespace VideoGameStore.Controllers 
{
    public class OrdersController : Controller 
    {
        private readonly IOrderService _orderService; //  variable para almacenar una instancia de órdenes
        private readonly ISessionService _sessionService; // variable para almacenar una instancia del servicio de sesión

        public OrdersController(IOrderService orderService, ISessionService sessionService)
        {
            _orderService = orderService; // Inicializa la variable del servicio de órdenes con la instancia proporcionada
            _sessionService = sessionService; // Inicializa la variable del servicio de sesión con la instancia proporcionada
        }

        // Muestra las órdenes del usuario actual
        public async Task<IActionResult> Index()
        {
            int userId = GetUserId(); // Obtiene el ID del usuario 
            var orders = await _orderService.GetOrdersByUserAsync(userId); // Obtiene las órdenes del usuario actual a través del servicio
            return View(orders); // Devuelve la vista con las órdenes del usuario
        }

        // Muestra los detalles de una orden
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id); // Obtiene los detalles de la orden con el ID 
            if (order == null)
            {
                return NotFound(); // Si la orden no se encuentra, devuelve un error 404
            }

            return View(order); // Devuelve la vista con los detalles
        }

        //  Realiza un nuevo pedido para el usuario actual
        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            int userId = GetUserId(); // Obtiene el ID del usuario actual
            await _orderService.PlaceOrderAsync(userId); // Realiza un nuevo pedido a través del servicio
            return RedirectToAction("Index"); // Redirige a la acción Index después de realizar el pedido
        }

        // Obtiene el ID del usuario actual a partir de la sesión
        private int GetUserId()
        {
            return _sessionService.GetUserId(); // Obtiene el ID del usuario actual a través del servicio de sesión
        }
    }
}
