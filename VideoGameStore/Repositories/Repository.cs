using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions; 

namespace VideoGameStore.Repositories
{
    // Define la clase Repository que implementa la interfaz IRepository y utiliza un tipo genérico TEntity
    
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context; // Declara una instancia del contexto de la base de datos
        protected readonly DbSet<TEntity> DbSet; // Declara un conjunto de entidades del tipo TEntity

        // Constructor que inicializa  la base de datos y establece el DbSet
        public Repository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        // Método para obtener una entidad por su ID 
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        // Método para obtener todas las entidades 
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        // Método para encontrar entidades que cumplan con un predicado dado
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        // Método para obtener una única entidad que cumpla con un predicado dado  
        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.SingleOrDefaultAsync(predicate);
        }

        // Método para agregar una entidad de manera 
        public async Task AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        // Método para agregar un rango de entidades  
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);
            await Context.SaveChangesAsync();
        }

        // Método para actualizar 
        public void Update(TEntity entity)
        {
            DbSet.Update(entity);
            Context.SaveChanges();
        }

        // Método para actualizar un rango de entidades
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
            Context.SaveChanges();
        }

        // Método para eliminar una entidad
        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
            Context.SaveChanges();
        }

        // Método para eliminar un rango de entidades
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
            Context.SaveChanges();
        }
    }
}
