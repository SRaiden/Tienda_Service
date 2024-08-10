using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaService.api.CarritoCompras.Persistence;
using TiendaService.api.CarritoCompras.RemoteInterface;
using TiendaService.api.CarritoCompras.ViewModel;

namespace TiendaService.api.CarritoCompras.Application
{
    public class Consulta 
    {
        public class Ejecuta : IRequest<CarritoDto> { 
            public int CarritoSesionId { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta, CarritoDto>
        {
            private readonly DBContextCarrito _dBContextCarrito;
            private readonly ILibroService _libroService;

            public Manejador(DBContextCarrito dBContextCarrito, ILibroService libroService)
            {
                _dBContextCarrito = dBContextCarrito;
                _libroService = libroService;
            }

            public async Task<CarritoDto> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = await _dBContextCarrito.CarritoSesions.FirstOrDefaultAsync(x => x.CarritoSesionId == request.CarritoSesionId);
                var carritoSesionDetalle = await _dBContextCarrito.CarritoSesionDetalles.Where(d => d.CarritoSesionId == request.CarritoSesionId).ToListAsync();

                var lstCarritoDetalleDto = new List<CarritoDetalleDto>();

                foreach (var i in carritoSesionDetalle) {
                    var response = await _libroService.GetLibro(new Guid(i.ProductoSeleccionado));
                    if (response.resultado)
                    {
                        var objetoLibro = response.libroRemote;
                        var carritoDetalle = new CarritoDetalleDto { 
                            TituloLibro = objetoLibro.Titulo,
                            FechaPublicacion = objetoLibro.FechaPublicacion,
                            LibroId = objetoLibro.LibreriaMaterialId
                        };

                        lstCarritoDetalleDto.Add(carritoDetalle);
                    }
                }

                var carritoSesionDto = new CarritoDto { 
                    CarritoId = carritoSesion.CarritoSesionId,
                    FechaCreacionSesion = carritoSesion.FechaCreacion,
                    Lst_Productos = lstCarritoDetalleDto
                };

                return carritoSesionDto;
            }
        }
    }
}
