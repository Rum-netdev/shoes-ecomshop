using ShoesEShop.Data.Entities.Abstractions;

namespace ShoesEShop.Data.Entities
{
    public class Category : IEntityId, IEntityAuditable
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public IList<ProductCategory> ProductCategories { get; set; }
    }
}
