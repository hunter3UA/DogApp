using Microsoft.EntityFrameworkCore;

namespace DogApp.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder MigrateDatabase<TContext>(this IApplicationBuilder builder)
            where TContext : DbContext
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<TContext>();
                dbContext?.Database.Migrate();
            }

            return builder;
        }
    }
}