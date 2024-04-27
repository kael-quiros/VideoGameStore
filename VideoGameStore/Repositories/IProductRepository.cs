using VideoGameStore.Models;

namespace VideoGameStore.Repositories
{
    // Define una interfaz llamada IProductRepository que hereda de IRepository<Product>
    public interface IProductRepository : IRepository<Product>
    {
        //  obtiene todos los productos disponibles
        Task<IEnumerable<Product>> GetAvailableProductsAsync();

        // obtiene un producto  por su ID
        Task<Product> GetProductByIdAsync(int id);

        // Método para agregar un nuevo producto
        Task AddProductAsync(Product product);

        // Método para actualizar un producto 
        Task UpdateProductAsync(Product product);

        // Método para eliminar un producto 
        Task DeleteProductAsync(Product product);

        // Método para obtener todos los productos 
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}
