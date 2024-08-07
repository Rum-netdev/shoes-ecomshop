namespace ShoesEShop.Data.Entities.Abstractions
{
    public interface IEntityAuditable
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
