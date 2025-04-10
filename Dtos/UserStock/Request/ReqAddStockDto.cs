using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_Learning.Dtos.UserStock.Request
{
    public class ReqAddStockDto
    {
        [Required]
        public Guid stock_id { get; set; }
    }
}