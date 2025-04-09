using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore_Learning.Dtos.Comment.Request
{
    public class ReqCreateCommentDto
    {
        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters!")]
        public string Title { get; set; } = string.Empty;
        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 characters!")]
        public string Content { get; set; } = string.Empty;
        public Guid StockId { get; set; }
        public DateTime CreateOn { get; set; } = DateTime.Now;
    }
}