using Framework.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities;

namespace Order.Domain.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartEntity>> GetAll();
        Task<CartEntity> GetById(Guid id);
        Task<CartEntity> Add(CartEntity entity);
        Task<CartEntity> Update(CartEntity entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
    public class CartRepository : ICartRepository
    {
        private readonly OrderDbContext _context;
        private ContextAccessor _contextAccessor;
        public CartRepository(OrderDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = new ContextAccessor(contextAccessor);
            _context.Database.EnsureCreated();
        }
        public async Task<CartEntity> Add(CartEntity entity)
        {
            entity.ModifiedBy = _contextAccessor.Id;
            entity.CustomerId = _contextAccessor.Id;
            _context.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public async Task<IEnumerable<CartEntity>> GetAll()
        {
            try
            {
                var result = await _context.Set<CartEntity>()
                    .Include(o => o.Customer)
                    .ToListAsync();

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Error: {e.InnerException}");
            }
            return null;
        }

        public async Task<CartEntity> GetById(Guid id)
        {
            return await _context.Set<CartEntity>().FindAsync(id);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _context.SaveChangesAsync(cancellationToken);
                return result;
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Error: {e.InnerException}");
            }
            return 0;
        }

        public async Task<CartEntity> Update(CartEntity entity)
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
