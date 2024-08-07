namespace ShoesEShop.Core.Options.Configurations
{
    public class JwtConfigurationOption
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public string SecurityKey { get; set; }

    }
}
