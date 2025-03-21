namespace Backend_Examen_Edgar_S_J_M.Domain.Alumno
{
    public class AlumnoDto
    {
        public int AlumnoID { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
    }
}
