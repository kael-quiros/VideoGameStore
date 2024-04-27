using Microsoft.AspNetCore.Mvc;
using VideoGameStore.Models;
using VideoGameStore.Services;
using System.Threading.Tasks;
using VideoGameStore.Repositories;

namespace VideoGameStore.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISessionService _sessionService;
        private readonly IUserRepository _userRepository;
        public AuthController(IUserService userService, ISessionService sessionService)
        {
            _userService = userService;
            _sessionService = sessionService;
        }

        public IActionResult Login()
        {
            // Verifica si hay una sesión existente
            var username = _sessionService.GetUsername();
            if (!string.IsNullOrEmpty(username))
            {
                // Redirige al usuario si ya está autenticado
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            bool isAuthenticated = await _userService.AuthenticateUserAsync(username, password);
            if (isAuthenticated)
            {
                // Establece la sesión después de la autenticación exitosa
                _sessionService.SetUsername(username);
               
                // Obtiene el estado de administrador del usuario autenticado
                bool isAdmin = await _userService.IsUserAdmin(username);
                _sessionService.SetIsAdmin(isAdmin);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                // el caso de credenciales inválidas
                ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos.");
                return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                await _userService.RegisterUserAsync(user, user.Password);
                // Redirig después de registrar al usuario
                return RedirectToAction("Login");
            }
            return View(user);
        }

        public IActionResult Logout()
        {
            // Eliminar la sesión
            _sessionService.ClearSession();
            return RedirectToAction("Login");
        }
    }
}
