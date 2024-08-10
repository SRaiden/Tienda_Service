using Microsoft.EntityFrameworkCore;
using TiendaService.api.Autor.Model;

namespace TiendaService.api.Autor.Persistence
{
    public class DBContextAutor : DbContext
    {
        public DBContextAutor(DbContextOptions<DBContextAutor> options) : base(options) { }


        public DbSet<AutorLibro> autorLibros { get; set; }
        public DbSet<GradoAcademico> gradoAcademico { get; set; }
    }
}
