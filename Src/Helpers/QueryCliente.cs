using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Src.Helpers
{
    public class QueryCliente
    {
        public string? SortBy {get; set;} = string.Empty;
        public bool IsDescending {get; set;} = false;

    }
}