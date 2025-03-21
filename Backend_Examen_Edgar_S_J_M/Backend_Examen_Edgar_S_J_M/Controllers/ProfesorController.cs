using Backend_Examen_Edgar_S_J_M.Domain;
using Backend_Examen_Edgar_S_J_M.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Examen_Edgar_S_J_M.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly ProfesorRepository _repository;

        public ProfesorController(ProfesorRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProfesores()
        {
            var Profesors = await _repository.ObtenerProfesoresAsync();
            return Ok(Profesors);
        }
        [HttpPost]
        public async Task<IActionResult> CrearProfesor([FromBody] ProfesorCreateDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre) || string.IsNullOrWhiteSpace(request.Apellido))
            {
                return BadRequest("El nombre y apellido son obligatorios.");
            }

            bool creado = await _repository.CrearProfesorAsync(request.Nombre, request.Apellido);
            if (creado)
                return Ok("Profesor creado correctamente.");
            else
                return StatusCode(500, "Error al crear el profesor.");
        }
        [HttpPut("{profesorId}")]
        public async Task<IActionResult> ActualizarProfesor(int profesorId, [FromBody] ProfesorCreateDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Nombre) || string.IsNullOrWhiteSpace(request.Apellido))
            {
                return BadRequest("El nombre y apellido son obligatorios.");
            }

            bool actualizado = await _repository.ActualizarProfesorAsync(profesorId, request.Nombre, request.Apellido);
            if (actualizado)
                return Ok("Profesor actualizado correctamente.");
            else
                return NotFound("No se encontró el profesor para actualizar.");
        }
        [HttpDelete("{profesorId}")]
        public async Task<IActionResult> EliminarProfesor(int profesorId)
        {
            bool eliminado = await _repository.EliminarProfesorAsync(profesorId);
            if (eliminado)
                return Ok("Profesor eliminado correctamente.");
            else
                return NotFound("No se encontró el profesor para eliminar.");
        }
    }
}
