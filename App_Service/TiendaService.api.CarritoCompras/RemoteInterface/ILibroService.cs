using TiendaService.api.CarritoCompras.RemoteModel;

namespace TiendaService.api.CarritoCompras.RemoteInterface
{
    public interface ILibroService
    {
        Task<(bool resultado, LibroRemote libroRemote, string ErrorMessage)> GetLibro(Guid LibroId);
    }
}
