using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using Framework.Auth;
using Microsoft.AspNetCore.Http;

namespace Store.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryEntity>> GetAll();
        Task<CategoryEntity> GetById(Guid id);
        Task<CategoryEntity> Add(CategoryEntity entity);
        Task<CategoryEntity> Update(CategoryEntity entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
    public class CategoryRepository : ICategoryRepository
    {
        protected readonly StoreDbContext _context;
        private ContextAccessor _contextAccessor;
        //public CategoryRepository(StoreDbContext context)
        public CategoryRepository(StoreDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = new ContextAccessor(contextAccessor);
            _context.Database.EnsureCreated();  
        }
        public async Task<CategoryEntity> Add(CategoryEntity entity)
        {
            try
            {
                entity.ModifiedBy = _contextAccessor.Id;
                _context.Entry(entity).State = EntityState.Added;
                return entity;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }
            return null;
        }

        public async Task<IEnumerable<CategoryEntity>> GetAll()
        {
            //return await _context.Set<CategoryEntity>().ToListAsync();
            return await _context.Set<CategoryEntity>().Include(o => o.Products).ToListAsync();
        }

        public async Task<CategoryEntity?> GetById(Guid id)
        {
            return await _context.Set<CategoryEntity>()
                .Include(o => o.Products)
                .ThenInclude(c => c.Attribute)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<CategoryEntity> Update(CategoryEntity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            return entity;
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
