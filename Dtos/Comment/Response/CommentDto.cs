using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_Learning.Dtos.Comment.Response
{
    public class CommentDto
    {
        public Guid Comment_id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreateOn { get; set; } = DateTime.Now;
        public Guid? StockId { get; set; }
        // public string UserName { get; set; } = string.Empty;
        // public string User_id { get; set; } = string.Empty;
        // public string Email { get; set; } = string.Empty;
    }
}