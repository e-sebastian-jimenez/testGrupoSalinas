using Frontend_Examen_Edgar_S_J_M.Domain;

namespace Frontend_Examen_Edgar_S_J_M.Services
{
    public interface IProfesorService
    {
        public Task<List<ProfesorDto>?> GetProfesoresAsync();

        public Task<bool> AddProfesorAsync(ProfesorDto profesor);

        public Task<bool> UpdateProfesorAsync(ProfesorDto profesor);

        public Task<bool> DeleteProfesorAsync(int id);
    }
}
