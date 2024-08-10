namespace TiendaService.api.CarritoCompras.Model
{
    public class CarritoSesion
    {
        public int CarritoSesionId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public ICollection<CarritoSesionDetalle> LstcarritoSesionDetalles { get; set; }
    }
}
