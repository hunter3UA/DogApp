using DogApp.Іnfrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DogApp.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDogDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DogDbContext>(options => options.UseSqlServer(configuration["ConnectionStrings:DogDb"]));

            return services;
        }
    }
}
