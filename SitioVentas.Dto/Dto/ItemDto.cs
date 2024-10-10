namespace SitioVentas.Dto.Dto
{
    public class ItemDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string Codigo { get; set; }
        public string? Descripcion { get; set; }
        public int Tipo { get; set; }
        public int Subgrupo { get; set; }
        public string SubgrupoNombre { get; set; }
        public List<FotoDto> Fotos { get; set; }

        public DateTime? Creado { get; set; }
        public DateTime? Actualizado { get; set; }
        public bool Activo {  get; set; }
        public int Precio { get; set; }

        public int Cantidad { get; set; }
        public ItemDto() { }


    }
}
