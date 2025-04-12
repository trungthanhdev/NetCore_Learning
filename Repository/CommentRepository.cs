using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCore_Learning.Data;
using NetCore_Learning.Dtos.Comment.Request;
using NetCore_Learning.Dtos.Comment.Response;
using NetCore_Learning.Interfaces;
using NetCore_Learning.Mappers;
using NetCore_Learning.Models;

namespace NetCore_Learning.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<AppUser> _userManager;
        public CommentRepository(ApplicationDBContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ResCreateCommentDto> CreateComment(ReqCreateCommentDto reqComment, string user_id)
        {
            try
            {
                // System.Console.WriteLine($"userid nhan vao repo: {user_id}");
                var convertComment = reqComment.toComment();

                var user = await _userManager.FindByIdAsync(user_id);
                if (user == null) throw new Exception("User not found!");

                convertComment.AppUserId = user_id;

                await _context.Comment.AddAsync(convertComment);
                var res = await _context.SaveChangesAsync();
                if (res <= 0)
                {
                    System.Console.WriteLine("k co j !");
                    throw new Exception("k luu db dc");
                }
                //find comment
                var userComment = await _context.Comment
                    .Where(x => x.AppUserId == user_id && x.Comment_id == convertComment.Comment_id)
                    .Include(x => x.AppUser)
                    .FirstOrDefaultAsync();

                var response = new ResCreateCommentDto
                {
                    Comment_id = userComment!.Comment_id,
                    Title = userComment.Title,
                    Content = userComment.Content,
                    CreateOn = userComment.CreateOn,
                    StockId = userComment.StockId ?? Guid.Empty,
                    Username = userComment!.AppUser!.UserName ?? "",
                    Email = userComment.AppUser.Email ?? "",
                    User_id = userComment.AppUser.Id
                };

                return response;
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"Lỗi khi tạo bình luận: {ex.Message}");
                throw;
            }
        }

        public async Task<Comment> DeleteCommentAsync(Guid id)
        {
            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                throw new ArgumentException("Comment not found!");
            }
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<List<Comment>> GetAllComment()
        {
            var comments = await _context.Comment.ToListAsync();
            return comments;
        }

        public async Task<Comment?> GetById(Guid id)
        {
            var comment = await _context.Comment.FindAsync(id);
            System.Console.WriteLine(comment);
            return comment;
        }

        public async Task<Comment> UpdateComment(Guid id, ReqUpdateCommentDto reqUpdateComment)
        {
            var existedComment = await _context.Comment.FindAsync(id);

            if (existedComment == null)
            {
                throw new ArgumentException("Comment doesnot exist!");
            }
            else
            {
                existedComment.Content = reqUpdateComment.Content;
                existedComment.CreateOn = DateTime.UtcNow;
                existedComment.Title = reqUpdateComment.Title;

                await _context.SaveChangesAsync();
                return existedComment;
            }
        }
    }
}