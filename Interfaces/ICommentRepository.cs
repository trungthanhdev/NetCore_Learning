using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCore_Learning.Dtos.Comment.Request;
using NetCore_Learning.Dtos.Comment.Response;
using NetCore_Learning.Models;

namespace NetCore_Learning.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllComment();
        Task<ResCreateCommentDto> CreateComment(ReqCreateCommentDto reqComment, string user_id);
        Task<Comment?> GetById(Guid id);
        Task<Comment> UpdateComment(Guid id, ReqUpdateCommentDto reqUpdateComment);
        Task<Comment> DeleteCommentAsync(Guid id);
    }
}