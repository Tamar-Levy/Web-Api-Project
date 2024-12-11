using Microsoft.EntityFrameworkCore;
using Repositories;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<MyShop215736745Context>(option => option.UseSqlServer("Server=SRV2\\PUPILS;Database=MyShop_215736745;Trusted_Connection=True;TrustServerCertificate=True"));

builder.Services.AddTransient<IUsersRepository,UsersRepository>();

builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddTransient<IProductsRepository, ProductsRepository>();

builder.Services.AddTransient<IProductsService, ProductsService>();

builder.Services.AddTransient<ICategoriesRepository, CategoriesRepository>();

builder.Services.AddTransient<ICategoriesService, CategoriesService>();

builder.Services.AddTransient<IOrdersRepository, OrdersRepository>();

builder.Services.AddTransient<IOrdersService, OrdersService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
