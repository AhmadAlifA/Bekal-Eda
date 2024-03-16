using Order.Domain.Entities;

namespace Order.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<ProductEntity> Update(ProductEntity entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

    public class ProductRepository : IProductRepository
    {
        protected readonly OrderDbContext _context;
        public ProductRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<ProductEntity> Update(ProductEntity entity)
        {
            _context.Set<ProductEntity>().Update(entity);
            return entity;
        }
    }
}
