namespace TiendaService.api.CarritoCompras.RemoteModel
{
    public class LibroRemote
    {
        public Guid? LibreriaMaterialId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public DateTime? FechaPublicacion { get; set; }
        public Guid? AutorLibro { get; set; }
    }
}
