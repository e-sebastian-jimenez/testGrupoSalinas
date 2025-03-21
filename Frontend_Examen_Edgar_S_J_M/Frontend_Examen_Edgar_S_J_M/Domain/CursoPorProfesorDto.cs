namespace Frontend_Examen_Edgar_S_J_M.Domain
{
    public class CursoPorProfesorDto
    {
        public int CursoID { get; set; }
        public string NombreCurso { get; set; } = string.Empty;
        public string CodigoCurso { get; set; } = string.Empty;
        public int AlumnoID { get; set; }
        public string NombreAlumno { get; set; } = string.Empty;
        public string ApellidoAlumno { get; set; } = string.Empty;
    }
}
