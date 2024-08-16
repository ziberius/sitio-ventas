using System;
using System.Collections.Generic;
using System.Text;

namespace SitioVentas.Entities.Entities.Base
{
    public class Base
    {
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public string UsuarioCreador { get; set; }

        public string UsuarioActualizador { get; set; }

        public bool Activo { get; set; }
    }
}
