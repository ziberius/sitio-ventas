using Dapper.Contrib.Extensions;

namespace SitioVentas.Entities.Entities
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string? Identificador { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Contrasena { get; set; }
    }
}
