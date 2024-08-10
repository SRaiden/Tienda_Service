using Microsoft.EntityFrameworkCore;
using TiendaService.api.CarritoCompras.Model;

namespace TiendaService.api.CarritoCompras.Persistence
{
    public class DBContextCarrito : DbContext
    {
        public DBContextCarrito(DbContextOptions<DBContextCarrito> options) : base(options) {}

        public DbSet<CarritoSesion> CarritoSesions { get; set; }
        public DbSet<CarritoSesionDetalle> CarritoSesionDetalles { get; set; }
    }
}
