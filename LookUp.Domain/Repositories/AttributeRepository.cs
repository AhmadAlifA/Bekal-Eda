using LookUp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LookUp.Domain.Repositories
{
    public interface IAttributeRepository
    {
        Task<IEnumerable<AttributeEntity>> GetAll();
        Task<AttributeEntity> GetById(Guid id);
        Task<AttributeEntity> Add(AttributeEntity entity);
        Task<AttributeEntity> Update(AttributeEntity entity);
        Task<AttributeEntity> ChangeStatus(AttributeEntity entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
    public class AttributeRepository : IAttributeRepository
    {
        protected readonly LookUpDbContext _context;
        public AttributeRepository(LookUpDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        public async Task<AttributeEntity> Add(AttributeEntity entity)
        {
            //_context.Set<AttributeEntity>().Add(entity);
            _context.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public async Task<IEnumerable<AttributeEntity>> GetAll()
        {
            return await _context.Set<AttributeEntity>().ToListAsync();
        }

        public async Task<AttributeEntity> GetById(Guid id)
        {
            return await _context.Set<AttributeEntity>().FindAsync(id);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<AttributeEntity> Update(AttributeEntity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
            }catch (Exception e) {
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

        public Task<AttributeEntity> ChangeStatus(AttributeEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
