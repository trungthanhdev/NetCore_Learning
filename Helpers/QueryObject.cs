using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_Learning.Helpers
{
    public class QueryObject
    {
        [MaxLength(20)]
        public string? Symbol { get; set; } = null;
        [MaxLength(20)]
        public string? CompanyName { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool isDescending { get; set; } = false;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 3;

    }
}