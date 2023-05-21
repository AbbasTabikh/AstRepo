using Demo.Data.Data;
using Demo.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Demo.Data.Configurations
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDemoDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Demo.Data")));
            return services;
        }


    }
}
