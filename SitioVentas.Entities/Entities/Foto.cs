using Dapper.Contrib.Extensions;

namespace SitioVentas.Entities.Entities
{
    [Table("foto")]
    public class Foto
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ruta { get; set; }
        public string Tipo { get; set; }
        public int Prioridad { get; set; }
        public int ItemId { get; set; }
    }
}
