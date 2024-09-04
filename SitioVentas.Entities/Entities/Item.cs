using Dapper.Contrib.Extensions;

namespace SitioVentas.Entities.Entities
{
    [Table("item")]
    public class Item: Base.Base
    {
        [Key]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string Codigo { get; set; }
        public int Tipo { get; set; }
        public string? Descripcion { get; set; }
        public int Precio { get; set; }
        public int Subgrupo { get; set; }
    }
}
