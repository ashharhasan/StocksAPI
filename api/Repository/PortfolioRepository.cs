using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        ApplicationDBContext _context;
        public PortfolioRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Portfolio?> CreatePortfolioAsync(string UserId, string Symbol)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Symbol.ToLower() == Symbol.ToLower());
            if (stock == null)
            {
                return null;
            }

            var portfolio = new Portfolio
            {
                AppUserId = UserId,
                StockId = stock.Id
            };

            _context.portfolios.Add(portfolio);
            await _context.SaveChangesAsync();

            return portfolio;
        }

        public async Task<Portfolio?> DeletePortfolioAsync(string UserId, string Symbol)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(x => x.Symbol.ToLower() == Symbol.ToLower());
            if (stock == null)
            {
                return null;
            }
            var portfolio = new Portfolio
            {
                AppUserId = UserId,
                StockId = stock.Id
            };

            _context.portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();

            return portfolio;
        }

        public async Task<List<Stock>> GetUserPortfolioAsync(AppUser user)
        {
            return await _context.portfolios.Where(u => u.AppUserId == user.Id)
            .Select(stock => new Stock
            {
                Id = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                Price = stock.Stock.Price,
                Comments = stock.Stock.Comments
            }).ToListAsync();
        }
    }
}