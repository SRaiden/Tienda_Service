using Microsoft.EntityFrameworkCore;
using TiendaService.Api.Libro.Persistence;
using MediatR;
using TiendaService.Api.Libro.Application;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var conexion = builder.Configuration.GetConnectionString("ConexionSqlServer");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());
builder.Services.AddDbContext<DBContextLibreria>(opt => {
    opt.UseSqlServer(conexion);
});
builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
builder.Services.AddAutoMapper(typeof(Consulta.Ejecuta));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
