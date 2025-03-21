using Backend_Examen_Edgar_S_J_M.Domain;
using Backend_Examen_Edgar_S_J_M.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Examen_Edgar_S_J_M.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private readonly AlumnoRepository _repository;

        public AlumnoController(AlumnoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Obteneralumnos()
        {
            var alumnos = await _repository.ObtenerAlumnosAsync();
            return Ok(alumnos);
        }
        [HttpPost]
        public async Task<IActionResult> CrearAlumno([FromBody] AlumnoCreateDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre) || string.IsNullOrWhiteSpace(request.Apellido))
            {
                return BadRequest("El nombre y apellido son obligatorios.");
            }

            bool creado = await _repository.CrearAlumnoAsync(request.Nombre, request.Apellido, request.FechaNacimiento);
            if (creado)
                return Ok("Alumno creado correctamente.");
            else
                return StatusCode(500, "Error al crear el alumno.");
        }
        [HttpPut("{alumnoId}")]
        public async Task<IActionResult> ActualizarAlumno(int alumnoId, [FromBody] AlumnoCreateDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre) || string.IsNullOrWhiteSpace(request.Apellido))
            {
                return BadRequest("El nombre y apellido son obligatorios.");
            }

            bool actualizado = await _repository.ActualizarAlumnoAsync(alumnoId, request.Nombre, request.Apellido, request.FechaNacimiento);
            if (actualizado)
                return Ok("Alumno actualizado correctamente.");
            else
                return NotFound("No se encontró el alumno para actualizar.");
        }
        [HttpDelete("{alumnoId}")]
        public async Task<IActionResult> Eliminaralumno(int alumnoId)
        {
            bool eliminado = await _repository.EliminarAlumnoAsync(alumnoId);
            if (eliminado)
                return Ok("Alumno eliminado correctamente.");
            else
                return NotFound("No se encontró el alumno para eliminar.");
        }
    }
}
