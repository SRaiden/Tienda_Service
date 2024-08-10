using Microsoft.EntityFrameworkCore;
using TiendaService.api.Autor.Persistence;
using MediatR;
using TiendaService.api.Autor.Application;
using FluentValidation.AspNetCore;
using TiendaService.api.Autor.Controllers;

var builder = WebApplication.CreateBuilder(args);
var conexion = builder.Configuration.GetConnectionString("ConexionPostgresql");

// Add services to the container.
builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<New>());

builder.Services.AddRazorPages();
builder.Services.AddDbContext<DBContextAutor>(options => {
    options.UseNpgsql(conexion);
});
builder.Services.AddMediatR(typeof(New.Manejador).Assembly);
builder.Services.AddAutoMapper(typeof(Consulta.Manejador));

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
