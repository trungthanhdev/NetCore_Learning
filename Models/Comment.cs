using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_Learning.Models
{
    public class Comment
    {
        [Key]
        public Guid Comment_id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreateOn { get; set; } = DateTime.UtcNow;
        public Guid? StockId { get; set; }
        //navigation to Stock model, allow to . to stock attribute 
        public Stock? Stock { get; set; }
    }
}