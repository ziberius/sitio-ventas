using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitioVentas.Dto.Dto
{
    public class PaginatedFilteredRequestDto
    {
        public PaginatedInfo Paginacion { get; set; }

        public FilterDataRequestDto Filtros { get; set; }

    }
}
