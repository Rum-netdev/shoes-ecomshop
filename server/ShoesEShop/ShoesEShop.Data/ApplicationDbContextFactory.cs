using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ShoesEShop.Data
{
    internal class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(configuration.GetConnectionString("AuthSystemAppDefault"));

            return new ApplicationDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            // in some cases, .AddJsonFile("appsettings.json", optional: true); is appropriate
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
