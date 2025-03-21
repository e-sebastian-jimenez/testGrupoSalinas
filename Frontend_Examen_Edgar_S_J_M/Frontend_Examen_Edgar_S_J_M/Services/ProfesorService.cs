using Frontend_Examen_Edgar_S_J_M.Domain;
using Frontend_Examen_Edgar_S_J_M.Pages;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Frontend_Examen_Edgar_S_J_M.Services
{
    public class ProfesorService : IProfesorService
    {
        private readonly HttpClient _http;

        public ProfesorService(HttpClient http)
        {
            _http = http;
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

        public async Task<bool> AddProfesorAsync(ProfesorDto profesor)
        {
            var createDto = new ProfesorCreateDto() { Nombre = profesor.Nombre, Apellido = profesor.Apellido };
            var jsonContent = new StringContent(
            JsonSerializer.Serialize(createDto),
            Encoding.UTF8, 
            "application/json" 
            );

            var response = await _http.PostAsync("http://localhost:5215/api/Profesor", jsonContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateProfesorAsync(ProfesorDto profesor)
        {
            var updateDto = new ProfesorCreateDto() { Nombre = profesor.Nombre, Apellido = profesor.Apellido };
            var jsonContent = new StringContent(
            JsonSerializer.Serialize(updateDto),
            Encoding.UTF8,
            "application/json"
            );

            var response = await _http.PutAsync($"http://localhost:5215/api/Profesor/{profesor.ProfesorID}", jsonContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteProfesorAsync(int id)
        {
            var response = await _http.DeleteAsync($"http://localhost:5215/api/Profesor/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
