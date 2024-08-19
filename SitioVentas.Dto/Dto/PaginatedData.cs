using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SitioVentas.Dto.Dto
{
    public class PaginatedData<T> :PaginatedInfo
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalItems { get; set; }
    }
}
