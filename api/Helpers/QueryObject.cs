using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public class QueryObject
    {
        public decimal? Price { get; set; } = null;
        public string? CompanyName { get; set; } = null;

        public string? SortBy { get; set; }=null;

        public bool IsDescending { get; set; } =false;

        public int PageSize { get; set; } = 5;

        public int PageNumber { get; set; } = 1;

    }
}