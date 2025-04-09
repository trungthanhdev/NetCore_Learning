using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCore_Learning.Data;
using NetCore_Learning.Dtos.Stock.Request;
using NetCore_Learning.Helpers;
using NetCore_Learning.Interfaces;
using NetCore_Learning.Mappers;
using NetCore_Learning.Models;

namespace NetCore_Learning.Controllers
{
    [Route("api/v1/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDBContext context, IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStocks()
        {
            var stocks = await _stockRepo.GetAllStocksAsync();
            return Ok(stocks);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetStockById([FromRoute] Guid id)
        {
            var stock = await _stockRepo.GetStockByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.toStockDto());
        }

        [HttpPost("new-stock")]
        public async Task<IActionResult> CreateNewStockAsync([FromBody] ReqCreateStockDto newStock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var stockModel = newStock.toStockFromDto();
            var newStockModel = await _stockRepo.CreateNewStockAsync(stockModel);
            return CreatedAtAction(nameof(GetStockById), new { id = stockModel.Stock_id }, stockModel.toStockDto());
        }

        [HttpPut("update-stock/{id:guid}")]
        public async Task<IActionResult> UpdateStock([FromRoute] Guid id, [FromBody] ReqUpdateStockDto updateStock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var stockModel = await _stockRepo.UpdateStockAsync(id, updateStock);
            if (stockModel == null)
            {
                return NotFound();
            }
            return Ok(stockModel.toStockDto());
        }

        [HttpDelete("delete-stock/{id:guid}")]
        public async Task<IActionResult> DeleteStock([FromRoute] Guid id)
        {
            var stock = await _stockRepo.DeleteAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterStock([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var listStock = await _stockRepo.FilterStock(query);
            return Ok(listStock);
        }

    }
}