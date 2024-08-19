using Dapper.Contrib.Extensions;

namespace SitioVentas.Entities.Entities
{
    [Table("tipo")]
    public class Tipo: Base.Base
    {
        [Key]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Codigo { get; set; }
    }
}
