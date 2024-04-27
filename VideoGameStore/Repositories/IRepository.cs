using System.Linq.Expressions; 

namespace VideoGameStore.Repositories
{
    // Define una interfaz genérica IRepository<TEntity> con restricción de clase para TEntity
    public interface IRepository<TEntity> where TEntity : class
    {
        // obtiene una entidad por su ID de manera asíncrona
        Task<TEntity> GetByIdAsync(int id);

        // obtiene todas las entidades de manera asíncrona
        Task<IEnumerable<TEntity>> GetAllAsync();

        // Método para encontrar entidades que cumplan con un predicado dado
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        // Método para obtener una única entidad que cumpla con un predicado dado de manera asíncrona
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        // Método para agregar una entidad de manera asíncrona
        Task AddAsync(TEntity entity);

        // Método para agregar un rango de entidades de manera asíncrona
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        // Método para actualizar una entidad
        void Update(TEntity entity);

        // Método para actualizar un rango de entidades
        void UpdateRange(IEnumerable<TEntity> entities);

        // Método para eliminar una entidad
        void Remove(TEntity entity);

        // Método para eliminar un rango de entidades
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
