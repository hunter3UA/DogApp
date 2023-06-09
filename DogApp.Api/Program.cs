﻿using AspNetCoreRateLimit;
using DogApp.Api.Extensions;
using DogApp.Api.Middlewares;
using DogApp.Іnfrastructure.DbContexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().ConfigureApiBehavior();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddRateLimiting(builder.Configuration);
builder.Services.AddDogDbContext(builder.Configuration);
builder.Services.AddExternalServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.MigrateDatabase<DogDbContext>();
app.UseIpRateLimiting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
