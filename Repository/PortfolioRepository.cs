using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetCore_Learning.Data;
using NetCore_Learning.Dtos.Stock.Response;
using NetCore_Learning.Dtos.UserStock.Request;
using NetCore_Learning.Interfaces;
using NetCore_Learning.Mappers;
using NetCore_Learning.Models;

namespace NetCore_Learning.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDBContext _context;

        public PortfolioRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<string> AddStockToPortfolio(string User_id, ReqAddStockDto Stock_id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == User_id);
            if (user == null) return "User not found!";

            var stock = await _context.Stock.FirstOrDefaultAsync(x => x.Stock_id == Stock_id.stock_id);
            if (stock == null) return "Stock not found!";

            var existedUserStock = await _context.UserStock.AnyAsync(x => x.Stock_id == Stock_id.stock_id && x.User_id == User_id);
            if (existedUserStock) return "Stock already exists in portfolio!";

            var userStock = new UserStock
            {
                User_id = User_id,
                Stock_id = Stock_id.stock_id
            };
            await _context.UserStock.AddAsync(userStock);
            await _context.SaveChangesAsync();
            return "Add stock successfully!";
        }

        public async Task<string> DeleteStockFromPortfolio(string User_id, ReqAddStockDto Stock_id)
        {
            var userStokeExist = await _context.UserStock.FirstOrDefaultAsync(x => x.User_id == User_id);
            if (userStokeExist == null)
            {
                return "User not found!";
            }

            var userStoke = await _context.UserStock.FirstOrDefaultAsync(x => x.User_id == User_id && x.Stock_id == Stock_id.stock_id);
            if (userStoke == null) return "Stock not found!";

            _context.UserStock.Remove(userStoke);
            var result = await _context.SaveChangesAsync();
            return result > 0 ? "Remove successfully!" : "Remove failed!";
        }

        public async Task<List<StockDto>> GetAllPortfolioStock(string User_id)
        {
            var allStock = await _context.UserStock
                .Where(x => x.User_id == User_id && x.Stock != null)
                .Include(x => x.Stock)
                .ThenInclude(x => x.Comment)
                .Select(x => x.Stock!.toStockDto())
                .ToListAsync();
            // return allStock.Where(x => x.Comments != null).ToList();
            return allStock;
        }
    }
}