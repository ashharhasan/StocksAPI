using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<Stock>> GetUserPortfolioAsync(AppUser appuser);
        Task<Portfolio?> CreatePortfolioAsync(string UserId, string Symbol);
        Task<Portfolio?> DeletePortfolioAsync(string UserId, string Symbol);
    }
}