using AutoMapper;
using TiendaService.api.Autor.Model;
using TiendaService.api.Autor.ViewModel;

namespace TiendaService.api.Autor.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AutorLibro, AutorDto>();
        }
    }
}
