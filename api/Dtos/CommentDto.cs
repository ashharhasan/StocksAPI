using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int? StockId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string CreatedBy { get; set; }
    }
    public class CommentCreateRequest
    {
        [Required]
        [MaxLength(20,ErrorMessage ="Title can't be gretar than 20 characters")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MinLength(10,ErrorMessage = "Comment can't be less than 10 characters")]
        public string Content { get; set; } = string.Empty;

        public string AppUserId { get; set; }
    }

    public class CommentUpdateRequest
    {
        [Required]
        [MaxLength(20,ErrorMessage ="Title can't be gretar than 20 characters")]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        [MinLength(10,ErrorMessage = "Comment can't be less than 10 characters")]
        public string Content { get; set; } = string.Empty;
        
        public string AppUserId { get; set; }
    }
    
}