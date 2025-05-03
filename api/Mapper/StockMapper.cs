using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;

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
    }
}