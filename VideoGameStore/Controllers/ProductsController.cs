using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore; 
using VideoGameStore.Models; 
using VideoGameStore.Services; 
using Microsoft.AspNetCore.Http; 

namespace VideoGameStore.Controllers 
{
    public class ProductsController : Controller 
    {
        private readonly IProductService _productService; //  variable para almacenar una instancia del servicio de productos
        private readonly ISessionService _sessionService; //  variable para almacenar una instancia del servicio de sesión

        public ProductsController(IProductService productService, ISessionService sessionService)
        {
            _productService = productService; // Inicializa la variable del servicio de productos 
            _sessionService = sessionService; // Inicializa la variable del servicio de sesión
        }

       // Muestra todos los productos disponibles
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProductsAsync(); // Obtiene todos los productos a través del servicio
            return View(products); // Devuelve la vista con la lista de productos
        }

        //  Muestra los detalles de un producto específico
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id); // Obtiene los detalles del producto con el ID proporcionado
            if (product == null)
            {
                return NotFound(); // Si el producto no se encuentra, devuelve un error 
            }

            return View(product); // Devuelve la vista con los detalles del producto
        }

        // Muestra el formulario para crear un nuevo producto
        public IActionResult Create()
        {
            if (!IsUserAdmin())
            {
                return Forbid(); // Si el usuario no es administrador, devuelve un error 403
            }

            ViewData["Title"] = "Crear nuevo producto"; 
            return View(); // Devuelve la vista para crear un nuevo producto
        }

        // Procesa la creación de un nuevo producto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile upload)
        {
            if (!IsUserAdmin())
            {
                return Forbid(); // Si el usuario no es administrador, devuelve un error 
            }

            if (ModelState.IsValid) // Verifica si el modelo es válido
            {
                try
                {
                    // Procesa la carga de la imagen si se proporciona un archivo
                    if (upload != null && upload.Length > 0)
                    {
                        byte[] imageData = null;
                        using (var memoryStream = new MemoryStream())
                        {
                            await upload.CopyToAsync(memoryStream);
                            imageData = memoryStream.ToArray();
                        }
                        product.Image = imageData;
                    }
                    else
                    {
                        product.Image = null;
                    }

                    // Crea el producto en la base de datos 
                    await _productService.CreateProductAsync(product);

                    return RedirectToAction("ProductManagement", "Admin"); // Redirige al panel de administración después de la creación 
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex); // Registra en la consola
                    ModelState.AddModelError("", "Ocurrió un error al guardar el producto. Intente de nuevo más tarde.");
                    return View(product); // Devuelve la vista con errores de validación
                }
            }

            ViewData["Title"] = "Crear nuevo producto"; 
            return View(product); // Devuelve la vista para crear un nuevo producto
        }

        //  Convierte y muestra la imagen de un producto en formato JPEG
        public async Task<IActionResult> ConvertirImagen(int id)
        {
            var product = await _productService.GetProductByIdAsync(id); // Obtiene el producto con el ID proporcionado
            if (product == null || product.Image == null)
            {
                return NotFound(); // Si el producto no se encuentra o no tiene imagen, devuelve un error 
            }

            return File(product.Image, "image/jpeg"); // Devuelve la imagen del producto en formato JPEG
        }

        // Muestra el formulario para editar un producto existente
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!IsUserAdmin())
            {
                return Forbid(); // Si el usuario no es administrador, devuelve un error 
            }

            var product = await _productService.GetProductByIdAsync(id); // Obtiene el producto con el ID 
            if (product == null)
            {
                return NotFound(); // Si el producto no se encuentra, devuelve un error 
            }

            ViewData["Title"] = "Editar producto"; 
            return View(product); // Devuelve la vista para editar el producto
        }

        //  Procesa la edición de un producto existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile upload)
        {
            if (!IsUserAdmin())
            {
                return Forbid(); // Si el usuario no es administrador, devuelve un error 
            }

            if (id != product.ProductId)
            {
                return NotFound(); // Si el ID del producto no coincide, devuelve un error 
            }

            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, mostrar los errores de validación
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View(product); // Devuelve la vista con errores de validación
            }

            try
            {
                // Actualiza la imagen del producto si se proporciona un archivo
                if (upload != null && upload.Length > 0)
                {
                    byte[] imageData = null;
                    using (var memoryStream = new MemoryStream())
                    {
                        await upload.CopyToAsync(memoryStream);
                        imageData = memoryStream.ToArray();
                    }
                    product.Image = imageData;
                }

                // Actualiza el producto en la base de datos a través del servicio
                await _productService.UpdateProductAsync(product);

                // Redirige al panel de administración después de la edición exitosa
                return RedirectToAction("ProductManagement", "Admin");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await ProductExists(product.ProductId)))
                {
                    return NotFound(); // Si el producto no existe, devuelve un error 
                }
                else
                {
                    throw;
                }
            }
        }

        // Muestra el formulario para eliminar un producto existente
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsUserAdmin())
            {
                return Forbid(); // Si el usuario no es administrador, devuelve un error 
            }

            var product = await _productService.GetProductByIdAsync(id); // Obtiene el producto con el ID 
            if (product == null)
            {
                return NotFound(); // Si el producto no se encuentra, devuelve un error 
            }

            ViewData["Title"] = "Eliminar producto"; // Configura el título de la vista
            return View(product); // Devuelve la vista para eliminar el producto
        }

        // Procesa la confirmación de eliminación de un producto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsUserAdmin())
            {
                return Forbid(); // Si el usuario no es administrador, devuelve un error 
            }

            var product = await _productService.GetProductByIdAsync(id); // Obtiene el producto con el ID 
            if (product == null)
            {
                return NotFound(); // Si el producto no se encuentra, devuelve un error 
            }

            await _productService.DeleteProductAsync(product); // Elimina el producto de la base de datos 
            return RedirectToAction("ProductManagement", "Admin"); // Redirige al panel de administración después de la eliminación 
        }

        //  Verifica si un producto existe en la base de datos
        private async Task<bool> ProductExists(int id)
        {
            var product = await _productService.GetProductByIdAsync(id); // Obtiene el producto con el ID 
            return product != null; // Devuelve true si el producto existe, false si no
        }

        // Verifica si el usuario actual es un administrador
        private bool IsUserAdmin()
        {
            return _sessionService.GetIsAdmin(); // Obtiene el estado de administrador del usuario actual a través del servicio de sesión
        }
    }
}
