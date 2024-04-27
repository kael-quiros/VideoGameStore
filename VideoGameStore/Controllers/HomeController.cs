using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using VideoGameStore.Services;

namespace VideoGameStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IProductService productService, IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _productService = productService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            bool isAdmin = false;

            // Obtiene el estado de administrador de la sesión
            if (HttpContext.Session.GetString("IsAdmin") != null)
            {
                isAdmin = Convert.ToBoolean(HttpContext.Session.GetString("IsAdmin"));
            }

            ViewData["IsAdmin"] = isAdmin;

            var products = await _productService.GetProductsAsync();
            return View(products);
        }

    }
}
