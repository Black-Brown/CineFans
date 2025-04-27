using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CineFans.Application.Contracts;
using CineFans.Application.Services;
using CineFans.Infrastructure.Repositories;
using CineFans.Infrastructure.Interface;
using CineFans.Domain.Entities;
using CineFans.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// ----------------------------
// Configuración de servicios
// ----------------------------

// DbContext
builder.Services.AddDbContext<CineFansDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CineFansDbContext")));

// Servicios personalizados
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


// Swagger y controladores
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ----------------------------
// Construcción de la app
// ----------------------------
var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication(); // 🔥 OBLIGATORIO porque ahora tenés Identity
app.UseAuthorization();

app.MapControllers();
app.Run();
