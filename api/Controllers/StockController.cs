using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Mapper;
using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController: ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList().Select(s=> s.ToDto());
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var stock = _context.Stocks.FirstOrDefault(s => s.Id == id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToDto());
        }

        [HttpPost]
        public IActionResult Create ([FromBody] CreateStockReqDto stockRequest)
        {
            var stock = stockRequest.ToModel();
            _context.Stocks.Add(stock);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stock.Id }, stock.ToDto());
        }

        // public IActionResult Create(Stock stock)
        // {
        //     var createdStock = context.Stocks.Add(stock);
        //     context.SaveChanges();
        //     return CreatedAtAction(nameof(GetById), new { id = createdStock.Entity.Id }, createdStock.Entity);
        // }
        // public IActionResult Update(int id, Stock stock)
        // {
        //     context.Stocks.Update(stock);
        //     context.SaveChanges();
        //     return NoContent();
        // }
        // public IActionResult Delete(int id)
        // {
        //     context.Stocks.Remove(new Stock { Id = id });
        //     context.SaveChanges();
        //     return NoContent();
        // }
    }
}