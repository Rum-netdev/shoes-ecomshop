using ShoesEShop.Core.Options.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AuthSystemApp.Core.Options
{
    public class JwtConfigurationOptionSetup : IConfigureOptions<JwtConfigurationOption>
    {
        public const string SectionName = "JwtConfiguration";
        private readonly IConfiguration _configuration;

        public JwtConfigurationOptionSetup(IConfiguration configuration)
            => _configuration = configuration;

        public void Configure(JwtConfigurationOption options)
        {
            _configuration.GetSection(SectionName)
                .Bind(options);
        }
    }
}
