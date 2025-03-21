using Frontend_Examen_Edgar_S_J_M.Domain;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;
using System.Text;

namespace Frontend_Examen_Edgar_S_J_M.Services
{
    public class AlumnoService : IAlumnoService
    {
        private readonly HttpClient _http;

        public AlumnoService(HttpClient http)
        {
            _http = http;
        }
        public async Task<bool> AddAlumnoAsync(AlumnoDto alumno)
        {
            var createDto = new AlumnoCreateDto() { Nombre = alumno.Nombre, Apellido = alumno.Apellido, FechaNacimiento = alumno.FechaNacimiento };
            var jsonContent = new StringContent(
            JsonSerializer.Serialize(createDto),
            Encoding.UTF8,
            "application/json"
            );

            var response = await _http.PostAsync("http://localhost:5215/api/Alumno", jsonContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAlumnoAsync(int id)
        {
            var response = await _http.DeleteAsync($"http://localhost:5215/api/Alumno/{id}");
            return response.IsSuccessStatusCode;
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

        public async Task<bool> UpdateAlumnoAsync(AlumnoDto alumno)
        {
            var updateDto = new AlumnoCreateDto() { Nombre = alumno.Nombre, Apellido = alumno.Apellido, FechaNacimiento = alumno.FechaNacimiento };
            var jsonContent = new StringContent(
            JsonSerializer.Serialize(updateDto),
            Encoding.UTF8,
            "application/json"
            );

            var response = await _http.PutAsync($"http://localhost:5215/api/Alumno/{alumno.AlumnoID}", jsonContent);
            return response.IsSuccessStatusCode;
        }
    }
}
