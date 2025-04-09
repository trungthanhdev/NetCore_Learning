using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCore_Learning.Data;
using NetCore_Learning.Dtos.Stock.Request;
using NetCore_Learning.Dtos.Stock.Response;
using NetCore_Learning.Helpers;
using NetCore_Learning.Interfaces;
using NetCore_Learning.Mappers;
using NetCore_Learning.Models;

namespace NetCore_Learning.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateNewStockAsync(Stock stockModel)
        {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(Guid id)
        {
            var stock = await _context.Stock.FindAsync(id);
            if (stock == null)
            {
                return null;
            }
            _context.Stock.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<List<StockDto>> FilterStock(QueryObject query)
        {
            var listStock = _context.Stock.Include(x => x.Comment).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.CompanyName))
            {
                listStock = listStock.Where(x => x.CompanyName.Contains(query.CompanyName));
            }
            if (!string.IsNullOrWhiteSpace(query.Symbol))
            {
                listStock = listStock.Where(x => x.Symbol.Contains(query.Symbol));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("MarketCap"))
                {
                    listStock = query.isDescending ? listStock.OrderByDescending(x => x.MarketCap) : listStock.OrderBy(x => x.MarketCap);
                }
            }
            var results = await listStock.Select(x => x.toStockDto()).ToListAsync();
            return results;

        }

        public async Task<List<StockDto>> GetAllStocksAsync(QueryObject query)
        {
            var stocks = _context.Stock.Include(c => c.Comment).AsQueryable();
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            stocks = stocks.OrderBy(x => x.Stock_id).Skip(skipNumber).Take(query.PageSize);
            var stockDto = stocks.Select(s => s.toStockDto());
            return await stockDto.ToListAsync();
        }

        public async Task<Stock?> GetStockByIdAsync(Guid id)
        {
            var stock = await _context.Stock.Include(c => c.Comment).FirstOrDefaultAsync(c => c.Stock_id == id);
            if (stock == null)
            {
                return null;
            }
            return stock;
        }

        public async Task<bool> StockExist(Guid id)
        {
            return await _context.Stock.AnyAsync(s => s.Stock_id == id);
        }

        public async Task<Stock?> UpdateStockAsync(Guid id, ReqUpdateStockDto updateStock)
        {
            var stockModel = await _context.Stock.FindAsync(id);
            if (stockModel == null)
            {
                return null;
            }
            stockModel.Symbol = updateStock.Symbol;
            stockModel.CompanyName = updateStock.CompanyName;
            stockModel.Pruchase = updateStock.Pruchase;
            stockModel.LastDiv = updateStock.LastDiv;
            stockModel.Industry = updateStock.Industry;
            stockModel.MarketCap = updateStock.MarketCap;

            await _context.SaveChangesAsync();
            return stockModel;
        }
    }
}