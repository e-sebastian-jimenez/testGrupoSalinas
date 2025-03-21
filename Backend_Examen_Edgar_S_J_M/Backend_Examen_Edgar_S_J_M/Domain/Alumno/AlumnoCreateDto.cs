using Microsoft.AspNetCore.Mvc;

namespace Backend_Examen_Edgar_S_J_M.Domain
{
    public class AlumnoCreateDto
    {
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
    }
}
