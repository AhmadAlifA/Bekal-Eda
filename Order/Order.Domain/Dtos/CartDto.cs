using Framework.Core.Enums;

namespace Order.Domain.Dtos
{
    public class CartDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public CartStatusEnum Status { get; set; }
        public List<CartProductDto?> CartProducts { get; set; }
        public UserDto Customer { get; set; }
    }
    public class CartCreatedUpdatedDto
    {
        public Guid? Id { get; set; }
        public CartStatusEnum? Status { get; set; } = CartStatusEnum.Pending;

    }
    public class CartStatusDto
    {
        public Guid Id { get; set; }
        public CartStatusEnum Status { get; set; }
    }
}
