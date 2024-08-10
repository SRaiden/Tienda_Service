using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TiendaService.api.Autor.Model;
using TiendaService.api.Autor.Persistence;
using TiendaService.api.Autor.ViewModel;

namespace TiendaService.api.Autor.Application
{
    public class ConsultaFiltro
    {
        public class AutorUnico : IRequest<AutorDto> { 
            public string AutorGuid { get; set; }
        }

        public class Manejador : IRequestHandler<AutorUnico, AutorDto>
        {
            public readonly DBContextAutor _dBContextAutor;
            private readonly IMapper _mapper;

            public Manejador(DBContextAutor dBContextAutor, IMapper mapper)
            {
                _dBContextAutor = dBContextAutor;
                _mapper = mapper;
            }

            public async Task<AutorDto> Handle(AutorUnico request, CancellationToken cancellationToken)
            {
                var autor = await _dBContextAutor.autorLibros.Where(d => d.AutorLibroGuid == request.AutorGuid).FirstOrDefaultAsync();
                if (autor == null) throw new Exception("No se encontro el autor");
                var autoresDto = _mapper.Map<AutorLibro, AutorDto>(autor);
                return autoresDto;
            }
        }
    }
}
