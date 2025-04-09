using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_Learning.Dtos.Stock.Request
{
    public class ReqCreateStockDto
    {
        [Required]
        [MaxLength(10)]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters!")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Last Pruchase must be non-negative.")]

        public decimal Pruchase { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Last Dividend must be non-negative.")]

        public decimal LastDiv { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters!")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1, long.MaxValue)]
        public long MarketCap { get; set; }
    }
}