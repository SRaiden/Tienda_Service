using Microsoft.EntityFrameworkCore;
using TiendaService.Api.Libro.Model;

namespace TiendaService.Api.Libro.Persistence
{
    public class DBContextLibreria : DbContext
    {
        public DBContextLibreria()
        {
            
        }

        public DBContextLibreria(DbContextOptions<DBContextLibreria> options) : base(options){}

        // virtual permite sobreescribir a futuro. O sea si queremos añadir nuevo campos a libreria material
        // virtual nos lo va a permitir
        public virtual DbSet<LibreriaMaterial> libreriaMaterials { get; set; } 
    }
}
