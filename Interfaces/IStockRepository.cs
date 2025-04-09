using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCore_Learning.Dtos.Stock.Request;
using NetCore_Learning.Dtos.Stock.Response;
using NetCore_Learning.Helpers;
using NetCore_Learning.Models;

namespace NetCore_Learning.Interfaces
{
    public interface IStockRepository
    {
        Task<List<StockDto>> GetAllStocksAsync(QueryObject query);
        Task<Stock?> GetStockByIdAsync(Guid id);
        Task<Stock> CreateNewStockAsync(Stock stockModel);
        Task<Stock?> UpdateStockAsync(Guid id, ReqUpdateStockDto updateStockDto);
        Task<Stock?> DeleteAsync(Guid id);
        Task<bool> StockExist(Guid id);
        Task<List<StockDto>> FilterStock(QueryObject query);
    }
}