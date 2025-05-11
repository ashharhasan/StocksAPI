using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class StockDto
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public decimal Price { get; set; }

    }
    public class CreateStockReqDto
    {
        [Required]
        [Length(3, 3, ErrorMessage = "Symbol must be of 3 digits")]
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;

        [Range(0.1, 100000, ErrorMessage = "Share price range is 0.1 to 100,000")]
        public decimal Price { get; set; }

    }

    public class UpdateStockReqDto
    {
        [Required]
        [Length(3, 3, ErrorMessage = "Symbol must be of 3 digits")]
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        
        [Range(0.1, 100000, ErrorMessage = "Share price range is 0.1 to 100,000")]
        public decimal Price { get; set; }

    }
}