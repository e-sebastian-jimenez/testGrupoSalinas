using Backend_Examen_Edgar_S_J_M.Domain;
using Backend_Examen_Edgar_S_J_M.Domain.Alumno;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace Backend_Examen_Edgar_S_J_M.Persistence.Repositories
{
    public class AlumnoRepository
    {
        private readonly SqlServerDbContext _context;

        public AlumnoRepository(SqlServerDbContext context)
        {
            _context = context;
        }

        public async Task<List<AlumnoDto>> ObtenerAlumnosAsync()
        {

            using (var connection = _context.Database.GetDbConnection())
            {
                var parameters = new { Operacion = "R" };
                var alumnos = await connection.QueryAsync<AlumnoDto>(
                    "sp_Alumnos_CRUD",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
                return alumnos.ToList();
            }
        }
        public async Task<bool> CrearAlumnoAsync(string nombre, string apellido, DateTime fechaNacimiento)
        {
            var operacionParam = new SqlParameter("@Operacion", "C");
            var alumnoIDParam = new SqlParameter("@AlumnoID", DBNull.Value);
            var nombreParam = new SqlParameter("@Nombre", nombre);
            var apellidoParam = new SqlParameter("@Apellido", apellido);
            var fechaNacimientoParam = new SqlParameter("@FechaNacimiento", fechaNacimiento);

            int rowsAffected = await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_Alumnos_CRUD @Operacion, @AlumnoID, @Nombre, @Apellido, @FechaNacimiento",
                operacionParam, alumnoIDParam, nombreParam, apellidoParam, fechaNacimientoParam
            );

            return rowsAffected > 0;
        }
        public async Task<bool> ActualizarAlumnoAsync(int id, string nombre, string apellido, DateTime fechaNacimiento)
        {
            var operacionParam = new SqlParameter("@Operacion", "U");
            var alumnoIDParam = new SqlParameter("@AlumnoID", id);
            var nombreParam = new SqlParameter("@Nombre", nombre);
            var apellidoParam = new SqlParameter("@Apellido", apellido);
            var fechaNacimientoParam = new SqlParameter("@FechaNacimiento", fechaNacimiento);

            int rowsAffected = await _context.Database.ExecuteSqlRawAsync("EXEC sp_Alumnos_CRUD @Operacion, @AlumnoID, @Nombre, " +
                "@Apellido, @FechaNacimiento", operacionParam, alumnoIDParam, nombreParam, apellidoParam, fechaNacimientoParam);

            return rowsAffected > 0;
        }
        public async Task<bool> EliminarAlumnoAsync(int id)
        {
            var operacionParam = new SqlParameter("@Operacion", "D");
            var alumnoIDParam = new SqlParameter("@AlumnoID", id);

            int rowsAffected = await _context.Database.ExecuteSqlRawAsync("EXEC sp_Alumnos_CRUD @Operacion, @AlumnoID", operacionParam, alumnoIDParam);
            return rowsAffected > 0;
        }
    }
}
