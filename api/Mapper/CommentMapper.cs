using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

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
                CreatedAt = comment.CreatedAt,
                CreatedBy = comment.CreatedBy.UserName
            };
        }
        public static Comment ToModel(this CommentUpdateRequest commentUpdateRequest, int stockID)
        {
            return new Comment
            {
                StockId = stockID,
                Title = commentUpdateRequest.Title,
                Content = commentUpdateRequest.Content,
                CreatedAt = DateTime.UtcNow,
                AppUserId = commentUpdateRequest.AppUserId
            };
        }
        public static Comment ToModel(this CommentCreateRequest commentUpdateRequest, int stockId)
        {
            return new Comment
            {
                StockId = stockId,
                Title = commentUpdateRequest.Title,
                Content = commentUpdateRequest.Content,
                CreatedAt = DateTime.UtcNow,
                AppUserId = commentUpdateRequest.AppUserId
            };
        }
        public static Comment CopyTo(this CommentUpdateRequest commentUpdateRequest, Comment comment)
        {
            comment.Title = commentUpdateRequest.Title;
            comment.Content = commentUpdateRequest.Content;
            comment.CreatedAt = DateTime.UtcNow;
            comment.AppUserId= commentUpdateRequest.AppUserId;
            return comment;
        }
    }
}