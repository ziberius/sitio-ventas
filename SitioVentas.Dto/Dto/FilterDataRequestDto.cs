using System;
using System.Collections.Generic;

namespace SitioVentas.Dto.Dto
{
    public class FilterDataRequestDto
    {
        public int Mes { get; set; }
        public int Year { get; set; }
        public int Dia { get; set; }
        public List<int> Grupo { get; set; }
        public List<int> Subgrupo { get; set; }
        public string Descripcion { get; set; }
        public int Tipo { get; set; }

        public string Nombre { get; set; }



    }
}
