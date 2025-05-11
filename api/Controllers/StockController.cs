using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepo;

        public StockController(IStockRepository stockRepo)
        {
            _stockRepo = stockRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepo.GetAllAsync();
            return Ok(stocks);
        }

        [HttpGet("top/{value}")]
        public async Task<IActionResult> GetTop(int value)
        {
            var stocks = await _stockRepo.GreaterThan(value);
            if (stocks == null)
            {
                return NotFound();
            }
            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var stock = await _stockRepo.GetByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockReqDto stockRequest)
        {
            var createdStock = await _stockRepo.CreateAsync(stockRequest);
            if (createdStock == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetById), new { id = createdStock.Id }, createdStock.ToDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockReqDto stockRequest)
        {

            var stock = await _stockRepo.UpdateAsync(id, stockRequest);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var stock = await _stockRepo.DeleteAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}