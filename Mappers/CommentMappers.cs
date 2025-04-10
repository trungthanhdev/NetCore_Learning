using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCore_Learning.Dtos.Comment.Request;
using NetCore_Learning.Dtos.Comment.Response;
using NetCore_Learning.Models;

namespace NetCore_Learning.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto toCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Comment_id = commentModel.Comment_id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreateOn = commentModel.CreateOn,
                StockId = commentModel.StockId,
                // UserName = commentModel!.AppUser!.UserName ?? "Unknown",
                // User_id = commentModel.AppUser.Id,
                // Email = commentModel.AppUser.Email ?? "Unknown"
            };
        }

        public static Comment toComment(this ReqCreateCommentDto commentModel)
        {
            return new Comment
            {
                Title = commentModel.Title,
                Content = commentModel.Content,
                StockId = commentModel.StockId
            };
        }
    }
}