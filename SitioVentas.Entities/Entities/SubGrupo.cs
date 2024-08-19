using Dapper.Contrib.Extensions;

namespace SitioVentas.Entities.Entities
{
    [Table("subgrupo")]
    public class SubGrupo: Base.Base
    {
        [Key]
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Codigo { get; set; }
        public int GrupoId { get; set; }
    }
}
