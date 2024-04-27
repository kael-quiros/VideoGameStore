using VideoGameStore.Models;
using Microsoft.EntityFrameworkCore; 
using VideoGameStore.Data; 

namespace VideoGameStore.Repositories
{
    // Define la clase ProductRepository que implementa la interfaz IProductRepository y hereda de Repository<Product>
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly VideoGameStoreContext _context; // Declara una instancia del contexto de la base de datos

        // Constructor que inicializa la clase con el contexto de la base de datos
        public ProductRepository(VideoGameStoreContext context) : base(context)
        {
            _context = context;
        }

        // Método para obtener un producto por su ID
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        // Método para obtener todos los productos 
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        // Método para agregar un producto 
        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        // Método para actualizar un producto 
        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        // Método para eliminar un producto
        public async Task DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        // Método para obtener todos los productos disponibles 
        public async Task<IEnumerable<Product>> GetAvailableProductsAsync()
        {
            return await _context.Products.Where(p => p.Availability).ToListAsync();
        }
    }
}
