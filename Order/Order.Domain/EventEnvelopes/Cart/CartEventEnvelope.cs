using Framework.Core.Enums;

namespace Order.Domain.EventEnvelopes.Cart
{
    public record CartCreated(
        Guid Id,
        CartStatusEnum Status
    )
    {
        public static CartCreated Create(Guid id,
            CartStatusEnum status) => new(id, status);
    }

    public record CartStatusChanged(
        Guid Id,
        List<CartProductItem> CartProducts,
        CartStatusEnum Status,
        decimal TotalPriceCart
        )
    {
        public static CartStatusChanged Create(Guid id, List<CartProductItem> cartProducts,
            CartStatusEnum status, decimal totalPriceCart) => new(id, cartProducts, status, totalPriceCart);
    }

    public class CartProductItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal Total;
    }

}
