using AutoMapper;
using GenFu;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiendaService.Api.Libro.Application;
using TiendaService.Api.Libro.Model;
using TiendaService.Api.Libro.Persistence;
using TiendaService.Api.Libro.ViewModel;
using Xunit;

namespace TiendaService.api.Libro.Test
{
    public class LibroServiceTest
    {
        private IEnumerable<LibreriaMaterial> ObtenerDataPrueba() {
            A.Configure<LibreriaMaterial>()
                .Fill(d => d.Titulo).AsArticleTitle() // en el campo titulo se le asignara un titulo aleatorio
                .Fill(d => d.LibreriaMaterialId, () => { return Guid.NewGuid(); }); // en el campo libreria material ID un guid nuevo

            var lista = A.ListOf<LibreriaMaterial>(30); // crearemos 30 elementos
            lista[0].LibreriaMaterialId = Guid.Empty;
            return lista;
        }

        private Mock<DBContextLibreria> CrearContexto() { 
            var dataPrueba = ObtenerDataPrueba().AsQueryable();
            var dbset = new Mock<DbSet<LibreriaMaterial>>();

            // declaramos que libreria material sea de tipo entidad.. hay que darle un provide, expression, elementType y GetEnumerator
            dbset.As<IQueryable<LibreriaMaterial>>().Setup(d => d.Provider).Returns(dataPrueba.Provider);
            dbset.As<IQueryable<LibreriaMaterial>>().Setup(d => d.Expression).Returns(dataPrueba.Expression);
            dbset.As<IQueryable<LibreriaMaterial>>().Setup(d => d.ElementType).Returns(dataPrueba.ElementType);
            dbset.As<IQueryable<LibreriaMaterial>>().Setup(d => d.GetEnumerator()).Returns(dataPrueba.GetEnumerator());

            dbset.As<IAsyncEnumerable<LibreriaMaterial>>()
                .Setup(d => d.GetAsyncEnumerator(new System.Threading.CancellationToken()))
                .Returns(new AsyncEnumerator<LibreriaMaterial>(dataPrueba.GetEnumerator()));

            //Esto sera posible para hacer filtros en libreria material
            dbset.As<IQueryable<LibreriaMaterial>>().Setup(d => d.Provider).Returns(new AsyncQueryProvider<LibreriaMaterial>(dataPrueba.Provider));

            var contexto = new Mock<DBContextLibreria>();
            contexto.Setup(d => d.libreriaMaterials).Returns(dbset.Object);
            return contexto;
        }

        [Fact]
        public async void GetLibroPorID() {
            var mockContexto = CrearContexto();
            var mappConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });

            var mapper = mappConfig.CreateMapper();
            var request = new ConsultaFiltro.LibroUnico();
            request.LibroId = Guid.Empty;

            var manejador = new ConsultaFiltro.Manejador(mockContexto.Object, mapper);
            var libro = await manejador.Handle(request, new System.Threading.CancellationToken());
            Assert.NotNull(libro); // Valida si nada es nulo
            Assert.True(libro.LibreriaMaterialId == Guid.Empty);
        }

        [Fact]
        public async void GetLibros() { // para ejecutar esto, seleccionar la palabra GetLibros y boton derecho y run test

            System.Diagnostics.Debugger.Launch();

            //Test para emular la clase Consulta del service Libros

            //1. Emular la instancia de entity framework core - dbContextLibreria
            //  para emular las acciones y eventos de un objeto en un ambiente unit test utilizamos objetos de tipo mock
            var mockContext = CrearContexto();

            //2.Emular al mapping Imapper
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingTest());
            });
            var mapper = mockMapper.CreateMapper();

            //3.Instanciar a la clase Manejador y pasarle como parametros los mocks que se crearon
            Consulta.Manejador manejador = new Consulta.Manejador(mockContext.Object, mapper);
            Consulta.Ejecuta request = new Consulta.Ejecuta();

            var lista = await manejador.Handle(request, new System.Threading.CancellationToken());
            Assert.True(lista.Any()); // assert.true retorna un bool si tuvo exito al realizar el test
        }

        [Fact]
        public async void GuardarLibro() {
            var options = new DbContextOptionsBuilder<DBContextLibreria>()
                                .UseInMemoryDatabase(databaseName: "MyBaseDatosLibro")
                                .Options;

            var contexto = new DBContextLibreria(options);
            var request = new Nuevo.Ejecuta();
            request.Titulo = "MyLibro";
            request.AutorLibro = Guid.Empty;
            request.FechaPublicacion = DateTime.Now;

            var manejador = new Nuevo.Manejador(contexto);
            var libro = await manejador.Handle(request, new System.Threading.CancellationToken());
            Assert.True(libro != null);
        }
    }
}
