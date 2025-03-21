namespace Frontend_Examen_Edgar_S_J_M.Domain
{
    public class CursoDto
    {
        public int CursoID { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public int? ProfesorID { get; set; }
    }
}
