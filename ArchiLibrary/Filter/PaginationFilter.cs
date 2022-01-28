using System;
using System.Collections.Generic;
using System.Text;

namespace ArchiLibrary.Filter
{
    public class PaginationFilter
    {
        // ? : convertir de force un attribut en un type 
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
