using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mapper;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        ApplicationDBContext _context;
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
        {
            var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();

            if (!String.IsNullOrWhiteSpace(queryObject.CompanyName))
            {
                stocks = stocks.Where(x => EF.Functions.ILike(x.CompanyName, $"%{queryObject.CompanyName}%"));
            }
            if (queryObject.Price != null)
            {
                stocks = stocks.Where(x => x.Price < queryObject.Price);
            }
            if (queryObject.SortBy != null)
            {
                // Step 1: Validate the sort field exists
                var property = typeof(Stock).GetProperty(queryObject.SortBy);
                if (property == null)
                    queryObject.SortBy = "Id"; // Fallback to default

                // Step 2: Use a switch expression (no null checks in the expression tree)
                stocks = queryObject.SortBy switch
                {
                    "CompanyName" => queryObject.IsDescending
                        ? stocks.OrderByDescending(x => x.CompanyName, StringComparer.OrdinalIgnoreCase)
                        : stocks.OrderBy(x => x.CompanyName, StringComparer.OrdinalIgnoreCase),
                    "Symbol" => queryObject.IsDescending
                        ? stocks.OrderByDescending(x => x.Symbol)
                        : stocks.OrderBy(x => x.Symbol),
                    "Price" => queryObject.IsDescending
                    ? stocks.OrderByDescending(x => x.Price)
                    : stocks.OrderBy(x => x.Price),
                    _ => queryObject.IsDescending
                    ? stocks.OrderByDescending(x => x.Id)
                    : stocks.OrderBy(x => x.Id)
                };
            }

            int skipNumber = (queryObject.PageNumber -1) * queryObject.PageSize;
            stocks= stocks.Skip(skipNumber).Take(queryObject.PageSize);

            return await stocks.ToListAsync();
        }

        public async Task<List<StockDto>> GreaterThan(int value)
        {
            var temp = _context.Stocks.Include(c => c.Comments);

            var stocks = await temp.Where(x => x.Price > value).Select(x => x.ToDto()).ToListAsync();

            return stocks;
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stock = await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);
            return stock;
        }

        public async Task<Stock?> CreateAsync(CreateStockReqDto stockRequest)
        {
            var stock = stockRequest.ToModel();
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            if (stock.Id == 0)
            {
                return null;
            }
            return stock;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockReqDto stockRequest)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null)
            {
                return null;
            }
            stock.CopyFrom(stockRequest);
            _context.Update(stock);
            await _context.SaveChangesAsync();

            return stock;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null)
            {
                return null;
            }
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }
    }
}