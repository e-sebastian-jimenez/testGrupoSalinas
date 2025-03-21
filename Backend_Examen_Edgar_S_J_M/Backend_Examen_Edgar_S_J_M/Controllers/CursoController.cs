using Backend_Examen_Edgar_S_J_M.Domain;
using Backend_Examen_Edgar_S_J_M.Persistence.Repositories;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Examen_Edgar_S_J_M.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly CursoRepository _repository;

        public CursoController(CursoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerCursos()
        {
            var cursos = await _repository.ObtenerCursosAsync();
            return Ok(cursos);
        }
        [HttpPost]
        public async Task<IActionResult> CrearCurso([FromBody] CursoCreateDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre) || string.IsNullOrWhiteSpace(request.Codigo))
            {
                return BadRequest("El nombre y el código son obligatorios.");
            }

            bool creado = await _repository.CrearCursoAsync(request.Nombre, request.Descripcion, request.Codigo);
            if (creado)
                return Ok("Curso creado correctamente.");
            else
                return StatusCode(500, "Error al crear el curso.");
        }
        [HttpPut("{cursoId}")]
        public async Task<IActionResult> ActualizarCurso(int cursoId, [FromBody] CursoCreateDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre) || string.IsNullOrWhiteSpace(request.Codigo))
            {
                return BadRequest("El nombre y el código son obligatorios.");
            }

            bool actualizado = await _repository.ActualizarCursoAsync(cursoId, request.Nombre, request.Descripcion, request.Codigo);
            if (actualizado)
                return Ok("Curso actualizado correctamente.");
            else
                return NotFound("No se encontró el curso para actualizar.");
        }
        [HttpDelete("{cursoId}")]
        public async Task<IActionResult> EliminarCurso(int cursoId)
        {
            bool eliminado = await _repository.EliminarCursoAsync(cursoId);
            if (eliminado)
                return Ok("Curso eliminado correctamente.");
            else
                return NotFound("No se encontró el curso para eliminar.");
        }
        [HttpPut("asignar-profesor")]
        public async Task<IActionResult> AsignarProfesor([FromBody] AsignarProfesorDto request)
        {
            await _repository.AsignarProfesorAsync(request.CursoID, request.ProfesorID);
            return Ok("Profesor asignado correctamente.");
        }
        [HttpPost("inscribir-alumno")]
        public async Task<IActionResult> InscribirAlumno([FromBody] InscripcionAlumnoDto request)
        {
            await _repository.InscribirAlumnoEnCurso(request.AlumnoID, request.CursoID);
            return Ok("Alumno inscrito correctamente.");
        }
        [HttpGet("alumnos-por-curso/{cursoId}")]
        public async Task<IActionResult> ObtenerAlumnosPorCurso(int cursoId)
        {
            var alumnos = await _repository.ConsultarAlumnosPorCurso(cursoId);
            return Ok(alumnos);
        }
        [HttpGet("cursos-por-profesor/{profesorId}")]
        public async Task<IActionResult> ConsultarCursosPorProfesor(int profesorId)
        {
            var cursos = await _repository.ConsultarCursosPorProfesor(profesorId);
            return Ok(cursos);
        }
    }
}
