using ShoesEShop.Data.Entities.Abstractions;

namespace ShoesEShop.Data.Entities
{
    public class Brand : IEntityId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Product> Products { get; set; }
    }
}
