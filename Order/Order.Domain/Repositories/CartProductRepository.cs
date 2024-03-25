using Framework.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Order.Domain.Dtos;
using Order.Domain.Entities;

namespace Order.Domain.Repositories
{
    public interface ICartProductRepository
    {
        Task<IEnumerable<CartProductEntity>> GetAll();
        Task<CartProductEntity> GetById(Guid id);
        Task<ProductEntity> GetProductById(Guid id);
        Task<IEnumerable<CartProductEntity>> GetCartById(Guid id);
        Task<CartProductEntity> GetByCartId(Guid id, Guid productId);
        Task<CartProductEntity> Add(CartProductEntity entity);
        Task<CartProductEntity> Update(CartProductEntity entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
    public class CartProductRepository : ICartProductRepository
    {
        private ContextAccessor _contextAccessor;
        private readonly OrderDbContext _context;
        public CartProductRepository(OrderDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = new ContextAccessor(contextAccessor);
            _context.Database.EnsureCreated();
        }
        public async Task<CartProductEntity> Add(CartProductEntity entity)
        {
            try
            {
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

        public async Task<IEnumerable<CartProductEntity>> GetAll()
        {
            try
            {
                var result = await _context.Set<CartProductEntity>()
                    .Include(o => o.Cart)
                    .Include(o => o.Product)
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

        public async Task<CartProductEntity> GetById(Guid id)
        {
            try
            {
                return await _context.Set<CartProductEntity>()
                    .Include(o => o.Cart)
                    .Include(o => o.Product)
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Error: {e.InnerException}");
            }
            return null;
        }


        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _context.SaveChangesAsync(cancellationToken);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Error: {e.InnerException}");
            }
            return 0;
        }

        public async Task<CartProductEntity> Update(CartProductEntity entity)
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

        public async Task<ProductEntity> GetProductById(Guid id)
        {
            try
            {
                return await _context.Set<ProductEntity>().FindAsync(id);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Error: {e.InnerException}");
            }
            return null;
        }
        public async Task<IEnumerable<CartProductEntity>> GetCartById(Guid id)
        {
            try
            {
                return await _context.Set<CartProductEntity>().Where(o => o.CartId == id).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Error: {e.InnerException}");
            }
            return null;
        }

        public async Task<CartProductEntity> GetByCartId(Guid id, Guid productId)
        {
            return await _context.Set<CartProductEntity>().Where(o => o.CartId == id && o.ProductId == productId).FirstOrDefaultAsync();
        }
    }
}
