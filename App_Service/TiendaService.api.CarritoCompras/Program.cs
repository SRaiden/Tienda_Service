using Microsoft.EntityFrameworkCore;
using TiendaService.api.CarritoCompras.Persistence;
using MediatR;
using TiendaService.api.CarritoCompras.Application;
using TiendaService.api.CarritoCompras.RemoteInterface;
using TiendaService.api.CarritoCompras.RemoteServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<DBContextCarrito>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("ConexionMySql"));
});
builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);

// Comunicacion entre otros services
builder.Services.AddHttpClient("Libros", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Libros"]);
});

// Añadir Intefaces
builder.Services.AddScoped<ILibroService, LibroService>();

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
