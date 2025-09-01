using Microsoft.EntityFrameworkCore;
using shopping_basket_api.Calculators;
using shopping_basket_api.Services;
using shopping_basket_api.SQL;
using shopping_basket_api.SQL.Repositories;
using shopping_basket_api.Startup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB setup
var connectionString = builder.Configuration["Database:ConnectionString"];
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(connectionString, opt => opt.EnableRetryOnFailure()), ServiceLifetime.Transient);

// repositories
builder.Services.AddSingleton<IBasketITemRepository, BasketItemRepository>();
builder.Services.AddSingleton<IProcessedBasketItemRepository, ProcessedBasketItemRepository>();
builder.Services.AddSingleton<IDiscountRepository, DiscountRepository>();

// calculators
builder.Services.AddTransient<IBasketCalculator<shopping_basket_api.SQL.Models.BasketItem>, BasketItemDiscountCalculator>();
builder.Services.AddTransient<IBasketCalculator<IEnumerable<shopping_basket_api.SQL.Models.BasketItem>>, BasketTotalCalculator>();

// services
builder.Services.AddSingleton<IShoppingService, ShoppingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ApplyMigrations();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
