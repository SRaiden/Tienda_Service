using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaService.Api.Libro.Model;
using TiendaService.Api.Libro.Persistence;
using TiendaService.Api.Libro.ViewModel;

namespace TiendaService.Api.Libro.Application
{
    public class Consulta
    {
        public class Ejecuta : IRequest<List<LibreriaMaterialDto>> {
            public Ejecuta()
            {
                
            }
        }

        public class Manejador : IRequestHandler<Ejecuta, List<LibreriaMaterialDto>>
        {
            private readonly DBContextLibreria _dBContextLibreria;
            private readonly IMapper _mapper;

            public Manejador(DBContextLibreria dBContextLibreria, IMapper mapper)
            {
                _dBContextLibreria = dBContextLibreria;
                _mapper = mapper;
            }

            public async Task<List<LibreriaMaterialDto>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libros = await _dBContextLibreria.libreriaMaterials.ToListAsync();
                var librosDto = _mapper.Map<List<LibreriaMaterial>, List<LibreriaMaterialDto>>(libros);
                return librosDto;
            }
        }
    }
}
