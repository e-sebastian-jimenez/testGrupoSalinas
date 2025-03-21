using Frontend_Examen_Edgar_S_J_M.Domain;

namespace Frontend_Examen_Edgar_S_J_M.Services
{
    public interface IAlumnoService
    {
        public Task<List<AlumnoDto>?> GetAlumnosAsync();

        public Task<bool> AddAlumnoAsync(AlumnoDto alumno);

        public Task<bool> UpdateAlumnoAsync(AlumnoDto alumno);

        public Task<bool> DeleteAlumnoAsync(int id);
    }
}
