using Dapper.Contrib.Extensions;

namespace SitioVentas.Entities.Entities
{
    [Table("tipo")]
    public class Tipo
    {
        [Key]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Codigo { get; set; }
        public DateTime Creado { get; set; }

        public DateTime? Actualizado { get; set; }

        public bool Activo { get; set; }
    }
}
