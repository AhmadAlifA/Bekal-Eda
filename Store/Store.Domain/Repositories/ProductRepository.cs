using Microsoft.EntityFrameworkCore;
using Store.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductEntity>> GetAll();
        Task<ProductEntity> GetById(Guid id);
        Task<ProductEntity> Add(ProductEntity entity);
        Task<ProductEntity> Update(ProductEntity entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _context;
        public ProductRepository(StoreDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<ProductEntity> Add(ProductEntity entity)
        {
            _context.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public async Task<IEnumerable<ProductEntity>> GetAll()
        {
            try
            {
                var result = await _context.Set<ProductEntity>()
                    .Include(o => o.Category)
                    .Include(o => o.Attribute)
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

        public async Task<ProductEntity> GetById(Guid id)
        {
            return await _context.Set<ProductEntity>().FindAsync(id);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<ProductEntity> Update(ProductEntity entity)
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
