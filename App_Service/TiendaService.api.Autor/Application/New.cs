using FluentValidation;
using MediatR;
using TiendaService.api.Autor.Model;
using TiendaService.api.Autor.Persistence;

namespace TiendaService.api.Autor.Application
{
    public class New
    {
        public class Ejecutar : IRequest { 
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }

        // Esto es de fluent. Sirve para validaciones
        public class EjecutarValidacion : AbstractValidator<Ejecutar> {
            public EjecutarValidacion()
            {
                RuleFor(x => x.Nombre).NotEmpty();
                RuleFor(x => x.Apellido).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecutar>
        {
            public readonly DBContextAutor _dBContextAutor;

            public Manejador(DBContextAutor dBContextAutor)
            {
                _dBContextAutor = dBContextAutor;
            }

            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var autorLibro = new AutorLibro { 
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    FechaNacimiento = request.FechaNacimiento,
                    AutorLibroGuid = Convert.ToString(Guid.NewGuid())
                };

                _dBContextAutor.autorLibros.Add(autorLibro);
                var valor = await _dBContextAutor.SaveChangesAsync();
                if (valor > 0) return Unit.Value;
                else throw new Exception("Error al insertar el autor");
            }
        }
    }
}
