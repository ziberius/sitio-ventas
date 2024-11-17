namespace SitioVentas.Dto.Dto
{
    public class MenuGrupoDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Codigo { get; set; }
        public List<MenuSubgrupoDto> menuSubgrupo {  get; set; }
        public MenuGrupoDto() { }


    }
}
