using FluentValidation;
using MediatR;
using TiendaService.Api.Libro.Model;
using TiendaService.Api.Libro.Persistence;

namespace TiendaService.Api.Libro.Application
{
    public class Nuevo
    {
        public class Ejecuta : IRequest { 
            public string Titulo { get; set; }
            public DateTime? FechaPublicacion { get; set; }
            public Guid? AutorLibro { get; set; }
        }

        public class EjecutaValidation : AbstractValidator<Ejecuta> {
            public EjecutaValidation()
            {
                RuleFor(d => d.Titulo).NotEmpty();
                RuleFor(d => d.FechaPublicacion).NotEmpty();
                RuleFor(d => d.AutorLibro).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly DBContextLibreria _dBContextLibreria;
            public Manejador(DBContextLibreria dBContextLibreria)
            {
                _dBContextLibreria = dBContextLibreria;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libro = new LibreriaMaterial
                {
                    Titulo = request.Titulo,
                    AutorLibro = request.AutorLibro,
                    FechaPublicacion = request.FechaPublicacion
                };

                _dBContextLibreria.libreriaMaterials.Add(libro);
                var value = await _dBContextLibreria.SaveChangesAsync();
                if (value > 0) return Unit.Value;
                else throw new Exception("No se pudo crear");
            }
        }
    }
}
