using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCore_Learning.Data;
using NetCore_Learning.Dtos.UserStock.Request;
using NetCore_Learning.Interfaces;

namespace NetCore_Learning.Controllers
{
    [ApiController]
    [Route("api/v1/portfolio")]
    public class PortfolioControlller : ControllerBase
    {
        private readonly IPortfolioRepository _portfoRepo;
        private readonly ApplicationDBContext _context;
        public PortfolioControlller(IPortfolioRepository portfoRepo, ApplicationDBContext context)
        {
            _portfoRepo = portfoRepo;
            _context = context;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllPortfolioStock([FromRoute] string id)
        {
            //log request
            var request = HttpContext.Request;
            // foreach (var header in request.Headers)
            // {
            //     System.Console.WriteLine(header);
            // }
            // foreach (var user in HttpContext.User.Claims)
            // {
            //     System.Console.WriteLine(user);
            // }
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            // var idd = User.FindFirst("sub")?.Value;
            // System.Console.WriteLine(idd);
            var di = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            System.Console.WriteLine($"di: {di}");
            var token_id = User.FindFirst("id")?.Value;
            System.Console.WriteLine($"token_id : {token_id}");
            // System.Console.WriteLine(email.ToString());
            var user1 = await _context.Users.FirstOrDefaultAsync(x => x.Id == email);
            System.Console.WriteLine(user1);

            var allStock = await _portfoRepo.GetAllPortfolioStock(id);
            if (allStock == null)
            {
                return Ok("No stock yet!");
            }
            return Ok(allStock);
        }

        [HttpPost("add-stock/{id}")]
        public async Task<IActionResult> AddStockToPortfolio([FromRoute] string id, [FromBody] ReqAddStockDto stock_idDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newStock = await _portfoRepo.AddStockToPortfolio(id, stock_idDto);
            return CreatedAtAction(nameof(GetAllPortfolioStock), new { id = id }, newStock);
        }

        [HttpDelete("remove-stock/{id}")]
        public async Task<IActionResult> DeleteStockFromPortfolio([FromRoute] string id, [FromBody] ReqAddStockDto stock_idDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var removeStock = await _portfoRepo.DeleteStockFromPortfolio(id, stock_idDto);
            // return Ok("Successfully!");
            if (removeStock == "UserStock not found!" || removeStock == "Stock not found!" || removeStock == "Remove failed!")
            {
                return NotFound(removeStock); // hoặc return BadRequest(result); tùy use-case
            }

            return Ok(removeStock);
        }
    }
}