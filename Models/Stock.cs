using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_Learning.Models
{
    public class Stock
    {
        [Key]
        public Guid Stock_id { get; set; } = Guid.NewGuid();
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Pruchase { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        //one to many relationship
        public List<Comment> Comment { get; set; } = new List<Comment>();
    }
}