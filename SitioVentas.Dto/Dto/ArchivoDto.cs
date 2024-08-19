using System;
using System.Collections.Generic;
using System.Text;

namespace SitioVentas.Dto.Dto
{
    public class ArchivoDto
    {
        public string ContenidoArchivoB64 { get; set; }
        public string Extension { get; set; }
        public string NombreArchivo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int ItemId { get; set; }
        public string Ruta { get; set; }

    }
}
