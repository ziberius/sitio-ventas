namespace SitioVentas.Dto.Dto
{
    public class SubgrupoDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Codigo { get; set; }
        public int GrupoId { get; set; }
        public SubgrupoDto() { }


    }
}
