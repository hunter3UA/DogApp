using DogApp.Api.Extensions;
using DogApp.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().ConfigureApiBehavior();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddFluentValidationBehaviour();
builder.Services.AddAutoMapper(ApplicationAssembly.GetAssembly());
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(ApplicationAssembly.GetAssembly()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDogDbContext(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
