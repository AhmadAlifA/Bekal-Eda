using Framework.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Payment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Domain.Repositories
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<PaymentEntity>> GetAll();
        Task<PaymentEntity> GetById(Guid id);
        Task<CartEntity> GetCartById(Guid cartId);
        Task<CartProductEntity> GetCartProductById(Guid cartProductId);
        Task<PaymentEntity> Add(PaymentEntity entity);
        Task<PaymentEntity> Update(PaymentEntity entity);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
    public class PaymentRepository: IPaymentRepository
    {
        private readonly PaymentDbContext _context;
        private ContextAccessor _contextAccessor;
        public PaymentRepository(PaymentDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = new ContextAccessor(contextAccessor);
            _context.Database.EnsureCreated();
        }
        public async Task<PaymentEntity> Add(PaymentEntity entity)
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
        public async Task<IEnumerable<PaymentEntity>> GetAll()
        {
            try
            {
                var result = await _context.Set<PaymentEntity>()
                    .Include(o => o.Cart)
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
        public async Task<PaymentEntity> GetById(Guid id)
        {
            try
            {
                return await _context.Set<PaymentEntity>().FindAsync(id);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Error: {e.InnerException}");
            }
            return null;
        }
        public async Task<CartEntity> GetCartById(Guid cartId)
        {
            try
            {
                return await _context.Set<CartEntity>().FindAsync(cartId);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Error: {e.InnerException}");
            }
            return null;
        }
        public async Task<CartProductEntity> GetCartProductById(Guid cartId)
        {
            try
            {
                return await _context.Set<CartProductEntity>().FindAsync(cartId);
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
        public async Task<PaymentEntity> Update(PaymentEntity entity)
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
