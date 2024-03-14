using Framework.Auth;
using Microsoft.EntityFrameworkCore;
using User.Domain.Entities;

namespace User.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserEntity>> GetAll();
        Task<UserEntity> Login(string username, string password);
        Task<UserEntity> Add(UserEntity entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
    public class UserRepository : IUserRepository
    {
        protected readonly UserDbContext _context;
        public UserRepository(UserDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        public async Task<UserEntity> Add(UserEntity entity)
        {
            //_context.Set<UserEntity>().Add(entity);
            _context.Entry(entity).State = EntityState.Added;
            return entity;
        }

        public async Task<IEnumerable<UserEntity>> GetAll()
        {
            return await _context.Set<UserEntity>().ToListAsync();
        }

        public async Task<UserEntity> Login(string username, string password)
        {
            return await _context.Set<UserEntity>()
                .FirstOrDefaultAsync(o => o.UserName == username && o.Password == Encryption.HashSha256(password))
                ?? new UserEntity();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
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
