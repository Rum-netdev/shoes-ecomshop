using ShoesEShop.Data.Entities.Abstractions;

namespace ShoesEShop.Data.Entities
{
    public class Product : IEntityId, IEntityAuditable
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public IList<ProductCategory> ProductCategories { get; set; }
    }
}
