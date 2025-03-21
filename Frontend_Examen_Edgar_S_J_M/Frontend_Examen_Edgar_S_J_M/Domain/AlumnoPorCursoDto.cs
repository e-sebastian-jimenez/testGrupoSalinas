namespace Frontend_Examen_Edgar_S_J_M.Domain
{
    public class AlumnoPorCursoDto
    {
        public int AlumnoID { get; set; }
        public string NombreAlumno { get; set; } = null!;
        public string ApellidoAlumno { get; set; } = null!;
        public int ProfesorID { get; set; }
        public string NombreProfesor { get; set; } = string.Empty;
        public string ApellidoProfesor { get; set; } = string.Empty;
    }
}
