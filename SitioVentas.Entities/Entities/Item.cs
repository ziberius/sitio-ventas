using Dapper.Contrib.Extensions;

namespace SitioVentas.Entities.Entities
{
    [Table("item")]
    public class Item
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int Tipo { get; set; }
        public string? Descripcion { get; set; }
    }
}
