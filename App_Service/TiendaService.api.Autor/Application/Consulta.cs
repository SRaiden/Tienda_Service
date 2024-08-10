using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaService.api.Autor.Model;
using TiendaService.api.Autor.Persistence;
using TiendaService.api.Autor.ViewModel;

namespace TiendaService.api.Autor.Application
{
    public class Consulta
    {
        public class ListaAutor : IRequest<List<AutorDto>> { 
            
        }

        public class Manejador : IRequestHandler<ListaAutor, List<AutorDto>>
        {
            public readonly DBContextAutor _dBContextAutor;
            private readonly IMapper _mapper;

            public Manejador(DBContextAutor dBContextAutor, IMapper mapper)
            {
                _dBContextAutor = dBContextAutor;
                _mapper = mapper;
            }

            public async Task<List<AutorDto>> Handle(ListaAutor request, CancellationToken cancellationToken)
            {
                var autores = await _dBContextAutor.autorLibros.ToListAsync();
                var autoresDto = _mapper.Map<List<AutorLibro>, List<AutorDto>>(autores);
                return autoresDto;
            }
        }
    }
}
