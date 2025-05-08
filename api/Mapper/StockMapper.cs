using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;

namespace api.Mapper
{
    public static class StockMapper
    {
        public static StockDto ToDto(this Models.Stock stock)
        {
            return new StockDto
            {
                Id = stock.Id,
                Symbol = stock.Symbol,
                CompanyName = stock.CompanyName,
                Price = stock.Price
            };
        }

        public static Stock ToModel(this CreateStockReqDto stockReq)
        {
            return new Stock
            {
                Symbol = stockReq.Symbol,
                CompanyName = stockReq.CompanyName,
                Price = stockReq.Price
            };
        }
        public static Stock ToModel(this UpdateStockReqDto stockReq)
        {
            return new Stock
            {
                Symbol = stockReq.Symbol,
                CompanyName = stockReq.CompanyName,
                Price = stockReq.Price
            };
        }
        public static void CopyFrom(this Stock stock, UpdateStockReqDto stockReq)
        {
            stock.Symbol = stockReq.Symbol;
            stock.CompanyName = stockReq.CompanyName;
            stock.Price = stockReq.Price;
        }
    }
}