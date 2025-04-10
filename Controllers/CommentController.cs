using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCore_Learning.Dtos.Comment.Request;
using NetCore_Learning.Interfaces;
using NetCore_Learning.Mappers;

namespace NetCore_Learning.Controllers
{
    [Route("api/v1/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;
        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComment()
        {
            var comments = await _commentRepo.GetAllComment();
            if (comments == null)
            {
                return Ok("null");
            }
            var commentDto = comments.Select(cmt => cmt.toCommentDto());
            return Ok(commentDto);
        }

        [HttpPost("do-comment")]
        [Authorize]
        public async Task<IActionResult> CreateComment([FromBody] ReqCreateCommentDto reqComment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user_id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            System.Console.WriteLine($"userid nhan vao: {user_id}");
            if (user_id == null) return Unauthorized();
            if (!await _stockRepo.StockExist(reqComment.StockId))
            {
                return NotFound("Stock does not exist!");
            }
            var comment = await _commentRepo.CreateComment(reqComment, user_id);
            return CreatedAtAction(nameof(GetById), new { id = comment.Comment_id }, comment);
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var comment = await _commentRepo.GetById(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.toCommentDto());
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateComment([FromRoute] Guid id, [FromBody] ReqUpdateCommentDto reqUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var updatedComment = await _commentRepo.UpdateComment(id, reqUpdateDto);
            return Ok(updatedComment.toCommentDto());
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid id)
        {
            await _commentRepo.DeleteCommentAsync(id);
            return Ok("Successfully!");
        }
    }
}