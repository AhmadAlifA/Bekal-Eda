using Framework.Core.Enums;
using Framework.Core.Events;
using Order.Domain.Entities;

namespace Order.Domain.Projection
{
    public record ProductCreated(
        Guid Id,
        string Sku,
        string Name,
        RecordStatusEnum Status);
    public record ProductUpdated(
    Guid Id,
    string Sku,
    string Name);
    public record ProductPriceVolumeChanged(Guid Id, decimal Price, decimal Volume);
    public record ProductStockSoldChanged(Guid Id, int Stock, int Sold);
    public record ProductStatusChanged(Guid Id, RecordStatusEnum Status);
    public class ProductProjection
    {
        public static bool Handle(EventEnvelope<ProductCreated> eventEnvelope)
        {
            var (id, sku, name, status) = eventEnvelope.Data;
            using (var context = new OrderDbContext(OrderDbContext.OnConfigure()))
            {
                ProductEntity entity = new ProductEntity()
                {
                    Id = (Guid)id,
                    Sku = sku,
                    Name = name,
                    Status = status
                };
                context.Add(entity);
                context.SaveChanges();
            }
            return true;
        }
        public static bool Handle(EventEnvelope<ProductUpdated> eventEnvelope)
        {
            try
            {
                var (id, sku, name) = eventEnvelope.Data;
                using (var context = new OrderDbContext(OrderDbContext.OnConfigure()))
                {
                    ProductEntity? entity = context.Set<ProductEntity>().Find(id);
                    entity.Sku = sku;
                    entity.Name = name;
                    context.Update(entity);

                    List<CartProductEntity> entityCart = context.Set<CartProductEntity>().Where(x => x.ProductId == id).ToList();
                    entityCart.ForEach(x =>
                    {
                        x.Sku = sku;
                        x.Name = name;

                        context.Entry(x).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    });
                    context.UpdateRange(entityCart);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }
            return true;
        }
        public static bool Handle(EventEnvelope<ProductPriceVolumeChanged> eventEnvelope)
        {
            try
            {
                var (id, price, volume) = eventEnvelope.Data;
                using (var context = new OrderDbContext(OrderDbContext.OnConfigure()))
                {
                    ProductEntity? entity = context.Set<ProductEntity>().Find(id);
                    entity.Price = price;
                    entity.Volume = volume;
                    context.Update(entity);

                    List<CartProductEntity> entityCart = context.Set<CartProductEntity>().Where(x => x.ProductId == id).ToList();
                    entityCart.ForEach(x =>
                    {
                        x.Price = price;

                        context.Entry(x).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    });
                    context.UpdateRange(entityCart);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }
            return true;
        }
        public static bool Handle(EventEnvelope<ProductStockSoldChanged> eventEnvelope)
        {
            try
            {
                var (id, stock, sold) = eventEnvelope.Data;
                using (var context = new OrderDbContext(OrderDbContext.OnConfigure()))
                {
                    ProductEntity? entity = context.Set<ProductEntity>().Find(id);
                    entity.Stock = stock;
                    entity.Sold = sold;
                    context.Update(entity);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }
            return true;
        }
        public static bool Handle(EventEnvelope<ProductStatusChanged> eventEnvelope)
        {
            try
            {
                var (id, status) = eventEnvelope.Data;
                using (var context = new OrderDbContext(OrderDbContext.OnConfigure()))
                {
                    ProductEntity? entity = context.Set<ProductEntity>().Find(id);
                    entity.Status = status;
                    context.Update(entity);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                Console.WriteLine($"Inner Error: {e.InnerException}");
            }

            return true;
        }
    }
}
