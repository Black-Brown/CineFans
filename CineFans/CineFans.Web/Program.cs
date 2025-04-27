using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using CineFans.Domain.Entities;
using CineFans.Application.Services;
using CineFans.Infrastructure.Repositories;
using CineFans.Infrastructure.Interface;
using CineFans.Application.Contracts;
using Microsoft.AspNetCore.Http.Features;
using CineFans.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------
// CONFIGURACIÓN DE SERVICIOS
// -------------------------------------
builder.Services.AddDbContext<CineFansDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CineFansDbContext")
    ?? throw new InvalidOperationException("Connection string 'CineFansDbContext' not found.")));


// Configurar otros servicios...
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

// Soporte para sesiones
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

// Soporte para controladores MVC + Vistas
builder.Services.AddControllersWithViews();

// Soporte para controladores tipo API
builder.Services.AddControllers(); // <- ¡Esto era lo que te faltaba!

builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddHttpClient("CineFansApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7263/api/");
});

// -------------------------------------
// REGISTRO DE SERVICIOS PERSONALIZADOS
// -------------------------------------
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 5 * 1024 * 1024; // 5MB
});

var app = builder.Build();

// -------------------------------------
// MIDDLEWARE
// -------------------------------------
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();


// Mapea controladores API
app.MapControllers();

// Mapea controladores MVC
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
