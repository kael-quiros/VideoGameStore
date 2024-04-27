using VideoGameStore.Models;
using VideoGameStore.Repositories;

namespace VideoGameStore.Services
{
    // Implementa de la interfaz IProductService para la gestión de productos
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        // Constructor que recibe el repositorio de productos
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // Obtiene un producto por su ID 
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        // Obtiene todos los productos 
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productRepository.GetProductsAsync();
        }

        // Crea un nuevo producto 
        public async Task CreateProductAsync(Product product)
        {
            await _productRepository.AddProductAsync(product);
        }

        // Actualiza un producto 
        public async Task UpdateProductAsync(Product product)
        {
            await _productRepository.UpdateProductAsync(product);
        }

        // Elimina un producto 
        public async Task DeleteProductAsync(Product product)
        {
            await _productRepository.DeleteProductAsync(product);
        }

        // Agrega un producto 
        public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddProductAsync(product);
        }

    }
}
