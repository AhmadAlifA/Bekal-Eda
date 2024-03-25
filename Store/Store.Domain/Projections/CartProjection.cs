using Framework.Core.Events;
using Store.Domain.Entities;

namespace Store.Domain.Projections;

public record CartStatusChanged(
    Guid Id,
    List<CartProductItem> CartProducts,
    CartStatusEnum Status
    );

public class CartProductItem
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Total;
}

public class CartProjection
{
    public static bool Handle(EventEnvelope<CartStatusChanged> eventEnvelope)
    {
        var (id, cartProducts, status) = eventEnvelope.Data;
        using (var context = new StoreDbContext(StoreDbContext.OnConfigure()))
        {
            foreach (var product in cartProducts)
            {
                var productDb = context.Products.Where(o => o.Id == product.ProductId).FirstOrDefault();
                if (product.ProductId == productDb.Id)
                    productDb.Stock = productDb.Stock - product.Quantity;
                context.Update(productDb);
                context.SaveChanges();
            }
        }
        return true;
    }
}
