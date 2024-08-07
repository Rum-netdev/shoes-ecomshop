namespace ShoesEShop.Handler.Products.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string BrandName { get; set; }
    }
}
