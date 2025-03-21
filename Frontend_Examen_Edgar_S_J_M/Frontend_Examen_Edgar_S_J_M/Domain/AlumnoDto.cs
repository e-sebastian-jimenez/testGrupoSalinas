namespace Frontend_Examen_Edgar_S_J_M.Domain
{
    public class AlumnoDto
    {
        public int AlumnoID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; } = DateTime.Now;
    }
}
