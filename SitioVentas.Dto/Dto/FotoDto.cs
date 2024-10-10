using System;
using System.Collections.Generic;
using System.Text;

namespace SitioVentas.Dto.Dto
{
    public class FotoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? Ruta { get; set; }
        public string Tipo { get; set; }
        public int Prioridad { get; set; }
        public string Archivo { get; set; }
        public int ItemId { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
