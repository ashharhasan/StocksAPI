using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Interfaces;
using api.Mapper;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController: ControllerBase
    {
        private readonly ICommentRepository _commentRepo;

        public CommentController(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var comments = await _commentRepo.GetAllAsync();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId([FromRoute]int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if(comment==null)
            {
                return NotFound();
            }
            return Ok(comment.ToDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create ([FromRoute] int stockId,[FromBody] CommentCreateRequest commentCreateRequest)
        {
            // if(!ModelState.IsValid)
            // {
            //     return ValidationProblem();
            // }
            var comment = await _commentRepo.CreateAsync(stockId,commentCreateRequest);

            if(comment == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetbyId),new {id = comment.Id},comment.ToDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update ([FromRoute] int id,[FromBody] CommentUpdateRequest commentUpdateRequest)
        {
            var comment = await _commentRepo.UpdateAsync(id,commentUpdateRequest);
            if(comment==null)
            {
                return NotFound();
            }
            return Ok(comment.ToDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var comment = await _commentRepo.DeleteAsync(id);
            if(comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }
    }
}