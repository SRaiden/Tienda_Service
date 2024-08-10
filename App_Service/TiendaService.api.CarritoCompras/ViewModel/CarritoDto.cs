namespace TiendaService.api.CarritoCompras.ViewModel
{
    public class CarritoDto
    {
        public int CarritoId { get; set; }
        public DateTime? FechaCreacionSesion { get; set; }
        public List<CarritoDetalleDto> Lst_Productos { get; set; } 
    }
}
