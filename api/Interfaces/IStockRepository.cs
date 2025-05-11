using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        public Task<List<Stock>> GetAllAsync();
        public Task<List<StockDto?>> GreaterThan(int value);
        public Task<Stock?> GetByIdAsync(int id);
        public Task<Stock?> CreateAsync(CreateStockReqDto stockRequest);
        public Task<Stock?> UpdateAsync(int id, UpdateStockReqDto stockRequest);
        public Task<Stock?> DeleteAsync(int id);
        
    }
}