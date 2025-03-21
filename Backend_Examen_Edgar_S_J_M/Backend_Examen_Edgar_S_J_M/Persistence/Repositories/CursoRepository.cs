using Backend_Examen_Edgar_S_J_M.Domain;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Backend_Examen_Edgar_S_J_M.Persistence.Repositories
{
    public class CursoRepository
    {
        private readonly SqlServerDbContext _context;

        public CursoRepository(SqlServerDbContext context)
        {
            _context = context;
        }

        public async Task<List<CursoDto>> ObtenerCursosAsync()
        {
            using (var connection = _context.Database.GetDbConnection())
            {
                var parameters = new { Operacion = "R" };
                var cursos = await connection.QueryAsync<CursoDto>(
                    "sp_Cursos_CRUD",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
                return cursos.ToList();
            }
        }
        public async Task<bool> CrearCursoAsync(string nombre, string descripcion, string codigo)
        {
            var operacionParam = new SqlParameter("@Operacion", "C");
            var cursoIDParam = new SqlParameter("@CursoID", DBNull.Value);
            var nombreParam = new SqlParameter("@Nombre", nombre);
            var descripcionParam = new SqlParameter("@Descripcion", descripcion);
            var codigoParam = new SqlParameter("@Codigo", codigo);

            int rowsAffected = await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_Cursos_CRUD @Operacion, @CursoID, @Nombre, @Descripcion, @Codigo",
                operacionParam, cursoIDParam, nombreParam, descripcionParam, codigoParam
            );

            return rowsAffected > 0;
        }

        public async Task<bool> ActualizarCursoAsync(int id, string nombre, string descripcion, string codigo)
        {
            var operacionParam = new SqlParameter("@Operacion", "U");
            var cursoIDParam = new SqlParameter("@CursoID", id);
            var nombreParam = new SqlParameter("@Nombre", nombre);
            var descripcionParam = new SqlParameter("@Descripcion", descripcion);
            var codigoParam = new SqlParameter("@Codigo", codigo);

            int rowsAffected = await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_Cursos_CRUD @Operacion, @CursoID, @Nombre, @Descripcion, @Codigo",
                operacionParam, cursoIDParam, nombreParam, descripcionParam, codigoParam
            );

            return rowsAffected > 0;
        }
        public async Task<bool> EliminarCursoAsync(int id)
        {
            var operacionParam = new SqlParameter("@Operacion", "D");
            var cursoIDParam = new SqlParameter("@CursoID", id);

            int rowsAffected = await _context.Database.ExecuteSqlRawAsync("EXEC sp_Cursos_CRUD @Operacion, @CursoID", operacionParam, cursoIDParam);

            return rowsAffected > 0;
        }
        public async Task AsignarProfesorAsync(int cursoID, int profesorID)
        {
            var cursoIDParam = new SqlParameter("@CursoID", cursoID);
            var profesorIDParam = new SqlParameter("@ProfesorID", profesorID);

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_AsignarProfesor @CursoID, @ProfesorID", cursoIDParam, profesorIDParam);
        }
        public async Task InscribirAlumnoEnCurso(int alumnoID, int cursoID)
        {
            var alumnoIDParam = new SqlParameter("@AlumnoID", alumnoID);
            var cursoIDParam = new SqlParameter("@CursoID", cursoID);

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_InscribirAlumno @AlumnoID, @CursoID", alumnoIDParam, cursoIDParam);
        }
        public async Task<List<AlumnoPorCursoDto>> ConsultarAlumnosPorCurso(int cursoID)
        {
            using (var connection = _context.Database.GetDbConnection())
            {
                var parameters = new { CursoID = cursoID };
                var alumnos = await connection.QueryAsync<AlumnoPorCursoDto>(
                    "sp_ConsultarAlumnosPorCurso",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
                return alumnos.ToList();
            }
        }
        public async Task<List<CursoPorProfesorDto>> ConsultarCursosPorProfesor(int profesorID)
        {
            using (var connection = _context.Database.GetDbConnection())
            {
                var parameters = new { ProfesorID = profesorID };
                var cursos = await connection.QueryAsync<CursoPorProfesorDto>(
                    "sp_ConsultarCursosPorProfesor",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
                return cursos.ToList();
            }
        }
    }
}
