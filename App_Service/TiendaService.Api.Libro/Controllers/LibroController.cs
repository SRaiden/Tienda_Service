using MediatR;
using Microsoft.AspNetCore.Mvc;
using TiendaService.Api.Libro.Application;
using TiendaService.Api.Libro.ViewModel;

namespace TiendaService.Api.Libro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : Controller
    {
        private readonly IMediator _mediator;
        public LibroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data) { 
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<LibreriaMaterialDto>>> getLigros() {
            return await _mediator.Send(new Consulta.Ejecuta());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LibreriaMaterialDto>> getLibro(Guid id) {
            return await _mediator.Send(new ConsultaFiltro.LibroUnico { LibroId = id });
        } 
    }
}
