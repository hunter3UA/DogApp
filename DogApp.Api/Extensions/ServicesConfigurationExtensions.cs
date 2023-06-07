using AspNetCoreRateLimit;
using DogApp.Application;
using DogApp.Application.Models;
using DogApp.Application.Repositories;
using DogApp.Іnfrastructure.DbContexts;
using DogApp.Іnfrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DogApp.Api.Extensions
{
    public static class ServicesConfigurationExtensions
    {
        public static IServiceCollection AddDogDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DogDbContext>(options => options.UseSqlServer(configuration["ConnectionStrings:DogDb"]));
            services.AddScoped<IDogRepository, DogRepository>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            return services;
        }

        public static IServiceCollection AddFluentValidationBehaviour(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(ApplicationAssembly.GetAssembly());

            return services;
        }

        public static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<IpRateLimitOptions>(configuration.GetSection(nameof(IpRateLimitOptions)));
            services.AddMemoryCache();
            services.AddInMemoryRateLimiting();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            return services;
        }

        public static IMvcBuilder ConfigureApiBehavior(this IMvcBuilder builder)
        {
            builder.ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(ms => ms.Value is not null && !ms.Value.Errors.IsNullOrEmpty())
                        .ToDictionary(kvp => kvp.Key, kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage)).ToArray();

                    var validationErrors = new List<ErrorModel>();

                    foreach (var error in errors)
                    {
                        foreach (var subError in error.Value)
                        {
                            validationErrors.Add(new ErrorModel { Key = error.Key, Message = subError });
                        }
                    }

                    var jsonSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new CamelCaseNamingStrategy()
                        }
                    };

                    var result = JsonConvert.SerializeObject(new ErrorResponse(validationErrors));

                    return new BadRequestObjectResult(result);
                };
            });

            return builder;
        }

        public static IServiceCollection AddExternalServices(this IServiceCollection services)
        {
            services.AddAutoMapper(ApplicationAssembly.GetAssembly());
            services.AddFluentValidationBehaviour();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(ApplicationAssembly.GetAssembly()));

            return services;
        }
    }

    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.Substring(0, 1).ToLower() + propertyName.Substring(1);
        }
    }
}