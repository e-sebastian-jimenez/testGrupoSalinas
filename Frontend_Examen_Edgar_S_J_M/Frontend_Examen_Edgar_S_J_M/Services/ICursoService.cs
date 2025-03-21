using Frontend_Examen_Edgar_S_J_M.Domain;
using Frontend_Examen_Edgar_S_J_M.Pages;
using System.Net.Http;

namespace Frontend_Examen_Edgar_S_J_M.Services
{
    public interface ICursoService
    {
        public Task<List<CursoDto>?> GetCursos();

        public Task<bool> AddCurso(CursoDto curso);

        public Task<bool> UpdateCurso(CursoDto curso);

        public Task<bool> DeleteCurso(int cursoId);
    }
}
