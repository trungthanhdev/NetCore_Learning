using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCore_Learning.Dtos.Stock.Response;
using NetCore_Learning.Dtos.UserStock.Request;
using NetCore_Learning.Models;

namespace NetCore_Learning.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<StockDto>> GetAllPortfolioStock(string User_id);
        Task<string> AddStockToPortfolio(string User_id, ReqAddStockDto Stock_id);
        Task<string> DeleteStockFromPortfolio(string User_id, ReqAddStockDto Stock_id);
    }
}