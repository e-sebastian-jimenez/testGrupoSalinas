using Frontend_Examen_Edgar_S_J_M.Domain;

namespace Frontend_Examen_Edgar_S_J_M.Services
{
    public interface IAsignacionService
    {
        public Task<List<ProfesorDto>?> GetProfesoresAsync();

        public Task<List<CursoDto>?> GetCursosAsync();

        public Task<bool> AsignarProfesorAsync(AsignarProfesorDto request);
    }
}
