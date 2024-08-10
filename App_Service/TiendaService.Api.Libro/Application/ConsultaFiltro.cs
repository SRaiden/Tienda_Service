using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaService.Api.Libro.Model;
using TiendaService.Api.Libro.Persistence;
using TiendaService.Api.Libro.ViewModel;

namespace TiendaService.Api.Libro.Application
{
    public class ConsultaFiltro
    {
        public class LibroUnico : IRequest<LibreriaMaterialDto> { 
            public Guid? LibroId { get; set; }
        }

        public class Manejador : IRequestHandler<LibroUnico, LibreriaMaterialDto>
        {
            private readonly DBContextLibreria _dBContextLibreria;
            private readonly IMapper _mapper;
            
            public Manejador(DBContextLibreria dBContextLibreria, IMapper mapper)
            {
                _dBContextLibreria = dBContextLibreria;
                _mapper = mapper;
            }

            public async Task<LibreriaMaterialDto> Handle(LibroUnico request, CancellationToken cancellationToken)
            {
                var libro = await _dBContextLibreria.libreriaMaterials.FirstOrDefaultAsync(d => d.LibreriaMaterialId == request.LibroId);
                if (libro == null) throw new Exception("No se encontro el libro");

                var libroDto = _mapper.Map<LibreriaMaterial, LibreriaMaterialDto>(libro);
                return libroDto;
            }
        }
    }
}
