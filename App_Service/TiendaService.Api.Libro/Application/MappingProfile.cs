using AutoMapper;
using TiendaService.Api.Libro.Model;
using TiendaService.Api.Libro.ViewModel;

namespace TiendaService.Api.Libro.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<LibreriaMaterial, LibreriaMaterialDto>();
        }
    }
}
