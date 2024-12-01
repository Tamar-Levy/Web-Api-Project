using Microsoft.EntityFrameworkCore;
using Repositories;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<MyShop215736745Context>(option => option.UseSqlServer("Server=SRV2\\PUPILS;Database=MyShop_215736745;Trusted_Connection=True;TrustServerCertificate=True"));

builder.Services.AddTransient<IUserRepository,UserRepository>();

builder.Services.AddTransient<IUserServices, UserServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
