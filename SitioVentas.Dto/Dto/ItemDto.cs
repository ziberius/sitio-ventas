namespace SitioVentas.Dto.Dto
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Type { get; set; }
        public int Subgrupo { get; set; }
        public List<FotoDto> Fotos { get; set; }

        public DateTime? Creado { get; set; }
        public DateTime? Actualizado { get; set; }
        public bool Activo {  get; set; }
        public ItemDto() { }


    }
}
