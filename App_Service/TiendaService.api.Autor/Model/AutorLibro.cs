namespace TiendaService.api.Autor.Model
{
    public class AutorLibro
    {
        public int AutorLibroId {  get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido {  get; set; } = string.Empty;
        public DateTime? FechaNacimiento { get; set; }
        public ICollection<GradoAcademico> LstgradoAcademicos { get; set; }
        public string AutorLibroGuid { get; set; }
    }
}
