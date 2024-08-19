using System;
using System.Collections.Generic;
using System.Text;

namespace SitioVentas.Entities.Entities.Base
{
    public class Base
    {
        public DateTime Creado { get; set; }

        public DateTime? Actualizado { get; set; }

        public bool Activo { get; set; }
    }
}
