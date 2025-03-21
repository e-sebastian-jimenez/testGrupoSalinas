using Frontend_Examen_Edgar_S_J_M.Domain;

namespace Frontend_Examen_Edgar_S_J_M.Services
{
    public interface IInscripcionService
    {
        public Task<List<AlumnoDto>?> GetAlumnosAsync();

        public Task<List<CursoDto>?> GetCursosAsync();

        public Task<bool> InscribirAlumnoAsync(InscripcionAlumnoDto request);
    }
}
