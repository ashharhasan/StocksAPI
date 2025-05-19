using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mapper;
using api.Migrations;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        ApplicationDBContext _context;

        public CommentRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            var comments = await _context.Comments.Include(a=>a.CreatedBy).ToListAsync();
            return comments;
        }

        public async Task<Comment?> CreateAsync(int stockId, CommentCreateRequest commentCreateRequest)
        {
            var comment = commentCreateRequest.ToModel(stockId);
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            if (comment.Id == 0)
            {
                return null;
            }
            return comment;
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            var comment = await _context.Comments.Include(a=>a.CreatedBy).FirstOrDefaultAsync(x => x.Id == id);
            return comment;
        }

        public async Task<Comment?> UpdateAsync(int stockId, CommentUpdateRequest commentUpdateRequest)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == stockId);
            if(comment == null)
            {
                return null;
            }
            comment.Content= "";
            commentUpdateRequest.CopyTo(comment);
            _context.Update(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if(comment == null)
            {
                return null;
            }

            _context.Remove(comment);
            await _context.SaveChangesAsync();

            return comment;
        }
    }
}