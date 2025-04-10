using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_Learning.Dtos.Comment.Response
{
    public class ResCreateCommentDto
    {
        public Guid Comment_id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateOn { get; set; }
        public Guid StockId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string User_id { get; set; }
    }
}