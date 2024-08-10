using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaService.api.CarritoCompras.Application;
using TiendaService.api.CarritoCompras.ViewModel;

namespace TiendaService.api.CarritoCompras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoCompraController : Controller
    {
        private readonly IMediator _mediator;
        public CarritoCompraController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data) {
            return await _mediator.Send(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarritoDto>> GetCarrito(int id) {
            return await _mediator.Send(new Consulta.Ejecuta { CarritoSesionId = id });
        }
    }
}
