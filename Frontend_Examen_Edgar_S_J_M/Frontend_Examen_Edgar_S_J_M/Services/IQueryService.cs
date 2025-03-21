using Frontend_Examen_Edgar_S_J_M.Domain;

namespace Frontend_Examen_Edgar_S_J_M.Services
{
    public interface IQueryService
    {
        public Task<List<AlumnoPorCursoDto>?> GetAlumnosByCursoId(int cursoId);

        public Task<List<CursoPorProfesorDto>?> GetCursosByProfesorId(int profesorId);

        public Task<List<CursoDto>?> GetCursosAsync();

        public Task<List<ProfesorDto>?> GetProfesoresAsync();
    }
}
