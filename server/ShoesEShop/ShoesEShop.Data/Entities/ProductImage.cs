using ShoesEShop.Data.Entities.Abstractions;
namespace ShoesEShop.Data.Entities
{
    public class ProductImage : IEntityId
    {
        public int Id { get; set; }
        public string Urls { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
