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
    }
}