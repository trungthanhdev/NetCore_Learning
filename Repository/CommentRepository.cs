using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using NetCore_Learning.Data;
using NetCore_Learning.Dtos.Comment.Request;
using NetCore_Learning.Interfaces;
using NetCore_Learning.Mappers;
using NetCore_Learning.Models;

namespace NetCore_Learning.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext _context;
        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Comment> CreateComment(ReqCreateCommentDto reqComment)
        {
            try
            {
                var convertComment = reqComment.toComment();
                await _context.Comment.AddAsync(convertComment);
                await _context.SaveChangesAsync();
                return convertComment;
            }
            catch (System.Exception)
            {
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