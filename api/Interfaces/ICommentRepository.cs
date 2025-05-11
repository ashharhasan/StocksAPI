using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        public Task<List<Comment>> GetAllAsync();
        public Task<Comment?> GetByIdAsync(int id);

        public Task<Comment?> CreateAsync(int stockId, CommentCreateRequest commentCreateRequest);
        public Task<Comment?> UpdateAsync(int stockId ,CommentUpdateRequest commentUpdateRequest);

        public Task<Comment?> DeleteAsync(int id);

    }
}