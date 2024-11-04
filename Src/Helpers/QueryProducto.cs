using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Src.Helpers
{
    public class QueryProducto
    {
        public string? TipoProducto {get; set;} = string.Empty;
        public bool IsDescendiente {get; set;} = false;
    }
}