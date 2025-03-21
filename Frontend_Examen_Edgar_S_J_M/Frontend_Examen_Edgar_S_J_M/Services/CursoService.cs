using Frontend_Examen_Edgar_S_J_M.Domain;
using Frontend_Examen_Edgar_S_J_M.Pages;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Frontend_Examen_Edgar_S_J_M.Services
{
    public class CursoService : ICursoService
    {
        private readonly HttpClient _http;
        public CursoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> AddCurso(CursoDto curso)
        {
            var createDto = new CursoCreateDto() { Nombre = curso.Nombre, Descripcion = curso.Descripcion, Codigo = curso.Codigo };
            var jsonContent = new StringContent(
            JsonSerializer.Serialize(createDto),
            Encoding.UTF8,
            "application/json"
            );

            var response = await _http.PostAsync("http://localhost:5215/api/Curso", jsonContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCurso(int cursoId)
        {
            var response = await _http.DeleteAsync($"http://localhost:5215/api/Curso/{cursoId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CursoDto>?> GetCursos()
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

        public async Task<bool> UpdateCurso(CursoDto curso)
        {
            var updateDto = new CursoCreateDto() { Nombre = curso.Nombre, Descripcion = curso.Descripcion, Codigo = curso.Codigo };
            var jsonContent = new StringContent(
            JsonSerializer.Serialize(updateDto),
            Encoding.UTF8,
            "application/json"
            );

            var response = await _http.PutAsync($"http://localhost:5215/api/Curso/{curso.CursoID}", jsonContent);
            return response.IsSuccessStatusCode;
        }
    }
}
