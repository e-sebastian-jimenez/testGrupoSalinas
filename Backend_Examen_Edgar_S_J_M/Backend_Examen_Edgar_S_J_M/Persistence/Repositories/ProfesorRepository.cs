using Backend_Examen_Edgar_S_J_M.Domain;
using Backend_Examen_Edgar_S_J_M.Domain.Profesor;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Backend_Examen_Edgar_S_J_M.Persistence.Repositories
{
    public class ProfesorRepository
    {
        private readonly SqlServerDbContext _context;
        public ProfesorRepository(SqlServerDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProfesorDto>> ObtenerProfesoresAsync()
        {
            using (var connection = _context.Database.GetDbConnection())
            {
                var parameters = new { Operacion = "R" };
                var profesores = await connection.QueryAsync<ProfesorDto>(
                    "sp_Profesores_CRUD",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
                return profesores.ToList();
            }
        }
        public async Task<bool> CrearProfesorAsync(string nombre, string apellido)
        {
            var operacionParam = new SqlParameter("@Operacion", "C");
            var profesorIDParam = new SqlParameter("@ProfesorID", DBNull.Value);
            var nombreParam = new SqlParameter("@Nombre", nombre);
            var apellidoParam = new SqlParameter("@Apellido", apellido);

            int rowsAffected = await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_Profesores_CRUD @Operacion, @ProfesorID, @Nombre, @Apellido",
                operacionParam, profesorIDParam, nombreParam, apellidoParam
            );

            return rowsAffected > 0;
        }
        public async Task<bool> ActualizarProfesorAsync(int id, string nombre, string apellido)
        {
            var operacionParam = new SqlParameter("@Operacion", "U");
            var profesorIDParam = new SqlParameter("@ProfesorID", id);
            var nombreParam = new SqlParameter("@Nombre", nombre);
            var apellidoParam = new SqlParameter("@Apellido", apellido);

            int rowsAffected = await _context.Database.ExecuteSqlRawAsync("EXEC sp_Profesores_CRUD @Operacion, @ProfesorID, @Nombre, " +
                "@Apellido", operacionParam, profesorIDParam, nombreParam, apellidoParam);

            return rowsAffected > 0;
        }
        public async Task<bool> EliminarProfesorAsync(int id)
        {
            var operacionParam = new SqlParameter("@Operacion", "D");
            var profesorIDParam = new SqlParameter("@ProfesorID", id);

            int rowsAffected = await _context.Database.ExecuteSqlRawAsync("EXEC sp_Profesores_CRUD @Operacion, @ProfesorID", operacionParam, profesorIDParam);
            return rowsAffected > 0;
        }
    }
}
