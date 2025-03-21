using Frontend_Examen_Edgar_S_J_M.Domain;
using System.Net.Http.Json;
using System.Net;
using Frontend_Examen_Edgar_S_J_M.Pages;

namespace Frontend_Examen_Edgar_S_J_M.Services
{
    public class QueryService : IQueryService
    {
        private readonly HttpClient _http;
        public QueryService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<AlumnoPorCursoDto>?> GetAlumnosByCursoId(int cursoId)
        {
            var response = await _http.GetAsync($"http://localhost:5215/api/Curso/alumnos-por-curso/{cursoId}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var alumnos = await response.Content.ReadFromJsonAsync<List<AlumnoPorCursoDto>>();
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

        public async Task<List<CursoPorProfesorDto>?> GetCursosByProfesorId(int profesorId)
        {
            var response = await _http.GetAsync($"http://localhost:5215/api/Curso/cursos-por-profesor/{profesorId}");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var cursos = await response.Content.ReadFromJsonAsync<List<CursoPorProfesorDto>>();
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
