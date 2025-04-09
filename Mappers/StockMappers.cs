using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetCore_Learning.Dtos.Stock.Request;
using NetCore_Learning.Dtos.Stock.Response;
using NetCore_Learning.Models;

namespace NetCore_Learning.Mappers
{
    public static class StockMappers
    {
        public static StockDto toStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Stock_id = stockModel.Stock_id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Pruchase = stockModel.Pruchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comment.Select(c => c.toCommentDto()).ToList()
            };
        }

        public static Stock toStockFromDto(this ReqCreateStockDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Pruchase = stockDto.Pruchase,
                LastDiv = stockDto.LastDiv,
                Industry = stockDto.Industry,
                MarketCap = stockDto.MarketCap
            };
        }
    }
}