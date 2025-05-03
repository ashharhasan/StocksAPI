using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;

namespace api.Mapper
{
    public static class CommentMapper
    {
        public static CommentDto ToDto(this Models.Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                StockId = comment.StockId,
                Title = comment.Title,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt
            };
        }
    }
}