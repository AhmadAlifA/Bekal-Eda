//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Order.Domain.EventEnvelopes.CartProduct
//{
//    public record CartProductCreated
//    (
//        Guid Id,
//        Guid CartId,
//        Guid ProductId,
//        int Quantity
//        , string Sku,
//        string Name,
//        decimal Price
//    )
//    {
//        public static CartProductCreated Create(Guid id,
//            Guid cartId,
//            Guid productId,
//            int quantity
//            , string sku,
//            string name,
//            decimal price
//            ) => new(id, cartId,
//                productId, quantity
//                , sku, name, price
//                );
//    }

//    public record CartProductUpdated(
//        Guid Id,
//        int Quantity)
//    {
//        public static CartProductUpdated Create(Guid id,
//            int quantity) => new(id, quantity);
//    }

//    public record CartProductCartChanged(
//        Guid Id,
//        Guid CartId)
//    {
//        public static CartProductCartChanged Create(Guid id, Guid cartId) => new(id, cartId);
//    }

//    public record CartProductProductChanged(
//    Guid Id,
//    Guid ProductId)
//    {
//        public static CartProductProductChanged Create(Guid id, Guid productId) => new(id, productId);
//    }
//}
