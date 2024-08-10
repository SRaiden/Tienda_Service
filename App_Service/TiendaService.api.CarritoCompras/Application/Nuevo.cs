using MediatR;
using TiendaService.api.CarritoCompras.Model;
using TiendaService.api.CarritoCompras.Persistence;

namespace TiendaService.api.CarritoCompras.Application
{
    public class Nuevo
    {
        public class Ejecuta : IRequest { 
            public DateTime? FechaCreacionSesion { get; set; }
            public List<string> LstProductos { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly DBContextCarrito _dBContextCarrito;
            public Manejador(DBContextCarrito dBContextCarrito)
            {
                _dBContextCarrito = dBContextCarrito;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var carritoSesion = new CarritoSesion { 
                    FechaCreacion = request.FechaCreacionSesion
                };

                _dBContextCarrito.CarritoSesions.Add(carritoSesion);
                var value = await _dBContextCarrito.SaveChangesAsync();
                if (value == 0) throw new Exception("Error al crear el carrito");

                int id = carritoSesion.CarritoSesionId;

                // agregar los detalles del carrito
                foreach (var i in request.LstProductos) {
                    var carritoSesionDetalle = new CarritoSesionDetalle
                    {
                        FechaCreacion = DateTime.Now,
                        CarritoSesionId = id,
                        ProductoSeleccionado = i
                    };

                    _dBContextCarrito.CarritoSesionDetalles.Add(carritoSesionDetalle);
                }
                value = await _dBContextCarrito.SaveChangesAsync();
                if (value == 0) throw new Exception("Error al ingresar detalles del carrito");

                return Unit.Value;

            }
        }
    }
}
