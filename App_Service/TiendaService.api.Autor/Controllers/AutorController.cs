using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TiendaService.api.Autor.Application;
using TiendaService.api.Autor.Model;
using TiendaService.api.Autor.ViewModel;

namespace TiendaService.api.Autor.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AutorController : Controller
    {
        private readonly IMediator _mediator;

        public AutorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> NewAutor(New.Ejecutar data)
        {
            return await _mediator.Send(data);
        }

        [HttpGet]
        public async Task<ActionResult<List<AutorDto>>> GetListAutores()
        {
            return await _mediator.Send(new Consulta.ListaAutor());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorDto>> GetAutorLibro(string id)
        {
            return await _mediator.Send(new ConsultaFiltro.AutorUnico { AutorGuid = id });
        }
    }
}


