using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VideoGameStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using VideoGameStore.Services;

namespace VideoGameStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly ISessionService _sessionService;

        public AdminController(IUserService userService, IProductService productService, ISessionService sessionService)
        {
            _userService = userService;
            _productService = productService;
            _sessionService = sessionService;
        }

        // Método para la gestión de usuarios
        public async Task<IActionResult> UserManagement()
        {
            // Verifica si el usuario es administrador
            if (!IsUserAdmin())
            {
                return Forbid(); // Devolve 403 si el usuario no es administrador
            }
            ViewData["Title"] = "Gestión de Usuarios";
            var users = await _userService.GetUsersAsync();
            return View(users);
        }

      
        // Método para la gestión de productos
        public async Task<IActionResult> ProductManagement()
        {
            bool isAdmin = false;

            // Obteniene el estado de administrador de la sesión
            if (HttpContext.Session.GetString("IsAdmin") != null)
            {
                isAdmin = Convert.ToBoolean(HttpContext.Session.GetString("IsAdmin"));
            }
            ViewData["Title"] = "Gestión de Productos";

            var products = await _productService.GetProductsAsync();
            return View(products);
        }

        

        private bool IsUserAdmin()
        {
            return _sessionService.GetIsAdmin();
        }
    }
}
