using Frontend_Examen_Edgar_S_J_M.Domain;
using static System.Net.WebRequestMethods;
using System.Net;
using System.Net.Http.Json;
using Frontend_Examen_Edgar_S_J_M.Pages;
using System.Text.Json;
using System.Text;

namespace Frontend_Examen_Edgar_S_J_M.Services
{
    public class AsignacionService : IAsignacionService
    {
        private readonly HttpClient _http;

        public AsignacionService(HttpClient http)
        {
            _http = http;
        }
        public async Task<bool> AsignarProfesorAsync(AsignarProfesorDto request)
        {
            var jsonContent = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json"
            );

            var response = await _http.PutAsync($"http://localhost:5215/api/Curso/asignar-profesor", jsonContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CursoDto>?> GetCursosAsync()
        {
            var response = await _http.GetAsync("http://localhost:5215/api/Curso");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var cursos = await response.Content.ReadFromJsonAsync<List<CursoDto>>();
                if (cursos != null && cursos.Any())
                {
                    return cursos;
                }
            }
            return new();
        }

        public async Task<List<ProfesorDto>?> GetProfesoresAsync()
        {
            var response = await _http.GetAsync("http://localhost:5215/api/Profesor");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var profesores = await response.Content.ReadFromJsonAsync<List<ProfesorDto>>();
                if (profesores != null && profesores.Any())
                {
                    return profesores;
                }
            }
            return new();
        }
    }
}
