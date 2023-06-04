using DogApp.Api.Models;
using DogApp.Application.Dtos.Dog;
using DogApp.Application.Repositories;
using DogApp.Іnfrastructure.DbContexts;
using DogApp.Іnfrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace DogApp.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDogDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DogDbContext>(options => options.UseSqlServer(configuration["ConnectionStrings:DogDb"]));
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

            return services;
        }


        public static IServiceCollection AddFluentValidationBehaviour(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(AddDogDto).Assembly);
            services.AddFluentValidationAutoValidation();

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

                    var errorResponse = new List<ErrorModel>();

                    foreach (var error in errors)
                    {
                        foreach (var subError in error.Value)
                        {
                            errorResponse.Add(new ErrorModel { PropertyName = error.Key, Message = subError });
                        }
                    }
                    var result = JsonConvert.SerializeObject(errorResponse);

                    return new BadRequestObjectResult(result);
                };
            });

            return builder;
        }
    }
}
