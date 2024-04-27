using VideoGameStore.Models;

namespace VideoGameStore.Services
{
    // Interfaz que define los métodos de productos 
    public interface IProductService
    {
        // Obtiene un producto por su ID
        Task<Product> GetProductByIdAsync(int id);

        // Obtiene todos los productos disponibles 
        Task<IEnumerable<Product>> GetProductsAsync();

        // Crea un nuevo producto
        Task CreateProductAsync(Product product);

        // Actualiza un producto existente 
        Task UpdateProductAsync(Product product);

        // Elimina un producto 
        Task DeleteProductAsync(Product product);

        // Agrega un nuevo producto 
        Task AddProductAsync(Product product);
    }
}
