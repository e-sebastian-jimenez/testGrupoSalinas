using Frontend_Examen_Edgar_S_J_M.Domain;
using static System.Net.WebRequestMethods;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace Frontend_Examen_Edgar_S_J_M.Services
{
    public class InscripcionService : IInscripcionService
    {
        private readonly HttpClient _http;

        public InscripcionService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<AlumnoDto>?> GetAlumnosAsync()
        {
            var response = await _http.GetAsync("http://localhost:5215/api/Alumno");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var alumnos = await response.Content.ReadFromJsonAsync<List<AlumnoDto>>();
                if (alumnos != null && alumnos.Any())
                {
                    return alumnos;
                }
            }
            return new();
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

        public async Task<bool> InscribirAlumnoAsync(InscripcionAlumnoDto request)
        {
            var jsonContent = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json"
            );

            var response = await _http.PostAsync($"http://localhost:5215/api/Curso/inscribir-alumno", jsonContent);
            return response.IsSuccessStatusCode;
        }
    }
}
