using Dapper.Contrib.Extensions;

namespace SitioVentas.Entities.Entities
{
    [Table("item")]
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string Codigo { get; set; }
        public int Tipo { get; set; }
        public string? Descripcion { get; set; }
        public int Precio { get; set; }
        public int Subgrupo { get; set; }
        public int Cantidad { get; set; }

        public DateTime Creado { get; set; }

        public DateTime? Actualizado { get; set; }

        public bool Activo { get; set; }
    }
}
