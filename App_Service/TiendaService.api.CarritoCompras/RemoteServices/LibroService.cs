using System.Text.Json;
using TiendaService.api.CarritoCompras.RemoteInterface;
using TiendaService.api.CarritoCompras.RemoteModel;

namespace TiendaService.api.CarritoCompras.RemoteServices
{
    public class LibroService : ILibroService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
        public LibroService(IHttpClientFactory httpClientFactory, ILogger logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<(bool resultado, LibroRemote libroRemote, string ErrorMessage)> GetLibro(Guid LibroId)
        {
            try
            {
                var cliente = _httpClientFactory.CreateClient("Libros");
                var response = await cliente.GetAsync($"api/Libro/{LibroId}");
                if (response.IsSuccessStatusCode)
                {
                    var contenido = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var resultado = JsonSerializer.Deserialize<LibroRemote>(contenido, options);
                    return (true, resultado, "");
                }
                else {
                    return (false, null, response.ReasonPhrase)
                }
            }
            catch (Exception ex) {
                _logger.LogError(ex.Message);
                return (false, null, ex.Message);
            }
        }
    }
}
