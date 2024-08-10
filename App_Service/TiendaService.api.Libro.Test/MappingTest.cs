using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaService.Api.Libro.Model;
using TiendaService.Api.Libro.ViewModel;

namespace TiendaService.api.Libro.Test
{
    public class MappingTest : Profile
    {
        public MappingTest() {
            CreateMap<LibreriaMaterial, LibreriaMaterialDto>();
        }
    }
}
