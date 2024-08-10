namespace TiendaService.api.Autor.ViewModel
{
    public class AutorDto
    {
        public int AutorLibroId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string AutorLibroGuid { get; set; }
    }
}
