﻿using Framework.Core.Enums;
using HotChocolate.Authorization;
using Order.Domain.Dtos;
using Order.Domain.Services;

namespace Order.GarphQL.Schema.Mutation
{
    [ExtendObjectType(typeof(Mutation))]
    public class CartMutation
    {
        private readonly ICartService _service;
        public CartMutation(ICartService service)
        {
            _service = service;
        }

        //[Authorize]
        [Authorize(Roles = new[] { "administrator", "customer" })]
        public async Task<CartDto> AddAsync(CartCreatedUpdatedDto dto)
        {
            return await _service.AddCart(dto);
        }
       
        [Authorize(Roles = new[] { "administrator", "customer" })]
        public async Task<CartDto> ConfirmCartToPaymentAsync(Guid id)
        {
            CartDto dto = new CartDto();
            dto.Id = id;
            var result = await _service.ChangeStatusCart(id, CartStatusEnum.Confirmed);
            if (result)
                dto = await _service.GetCartById(id);

            return dto;
        }
    }
}
