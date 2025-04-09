using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCore_Learning.Dtos.Comment.Response;

namespace NetCore_Learning.Dtos.Stock.Response
{
    public class StockDto
    {
        public Guid Stock_id { get; set; } = Guid.NewGuid();
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Pruchase { get; set; }
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        public List<CommentDto> Comments { get; set; } = new();
    }
}