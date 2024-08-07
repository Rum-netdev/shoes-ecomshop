using ShoesEShop.Core.Options.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AuthSystemApp.Core.Options
{
    public class DbContextConfiguraitonSetup : IConfigureOptions<DbContextConfiguration>
    {
        private const string SectionName = "DbContextConfiguration";
        private readonly IConfiguration _configuration;

        public DbContextConfiguraitonSetup(IConfiguration configuration)
            => _configuration = configuration;

        public void Configure(DbContextConfiguration options)
        {
            _configuration.GetSection(SectionName)
                .Bind(options);
        }
    }
}
