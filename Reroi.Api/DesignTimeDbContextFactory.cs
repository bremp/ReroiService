using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Reroi.Data;
using System.IO;

namespace Reroi.Api
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ReroiContext>
    {
        public ReroiContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();            

            var connectionString = configuration["Data:ReroiConnection:ConnectionString"];

            var builder = new DbContextOptionsBuilder<ReroiContext>()
                .UseSqlServer(connectionString, b => b.MigrationsAssembly("Reroi.Api"));            

            return new ReroiContext(builder.Options);
        }
    }
}
