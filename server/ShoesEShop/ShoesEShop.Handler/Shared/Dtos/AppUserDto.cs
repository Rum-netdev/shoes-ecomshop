namespace ShoesEShop.Handler.Shared.Dtos
{
    public class AppUserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime Dob { get; set; }
    }
}
