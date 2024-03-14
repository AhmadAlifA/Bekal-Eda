using Framework.Core.Enums;
using Store.Domain.EventEvelopes.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.EventEvelopes.Product
{
    public record ProductCreated(
        Guid Id,
        Guid CategoryId,
        Guid AttributeId,
        string Sku,
        string Name,
        string Description,
        RecordStatusEnum Status)
    {
        public static ProductCreated Create(Guid id,
            Guid categoryId,
            Guid attributeId,
            string sku,
            string name,
            string description,
            RecordStatusEnum status) => new(id, categoryId, attributeId, sku, name, description, status);
    }

    public record ProductUpdated(
        Guid Id,
        string Sku,
        string Name,
        string Description)
    {
        public static ProductUpdated Create(Guid id,
            string sku,
            string name,
            string descrption) => new(id,sku, name, descrption);
    }

    public record ProductCategoryChanged(Guid Id, Guid CategoryId)
    {
        public static ProductCategoryChanged Create(Guid id, Guid categoryId) => new(id, categoryId);
    }

    public record ProductAttributeChanged(Guid Id, Guid AttributeId)
    {
        public static ProductAttributeChanged Create(Guid id, Guid attributeId) => new(id, attributeId);
    }

    public record ProductPriceVolumeChanged(Guid Id, decimal Price, decimal volume)
    {
        public static ProductPriceVolumeChanged Create(Guid id, decimal price, decimal volume) => new(id, price, volume);
    }

    public record ProductStockSoldChanged(Guid Id, int Stock, int Sold)
    {
        public static ProductStockSoldChanged Create(Guid id, int stock, int sold) => new(id, stock, sold);
    }

    public record ProductStatusChanged(Guid Id, RecordStatusEnum Status)
    {
        public static ProductStatusChanged Create(Guid id, RecordStatusEnum Status) => new(id, Status);
    }
}
