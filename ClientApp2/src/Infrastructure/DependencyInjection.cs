using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.SqlServer;
namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("DefaultConnection")["ConnectionString"];
            services.AddDbContext<ApplicationDbContext>(o =>
            {
                o.UseSqlServer(connectionString);
            });

            services.AddScoped<IDataSeeder, DataSeeder>();

            return services;
        }
    }
}
