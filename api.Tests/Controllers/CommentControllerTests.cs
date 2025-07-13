using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers;
using api.Dtos.Comment;
using api.Interfaces;
using api.Models;
using FakeItEasy;
using Shouldly;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace api.Tests.Controllers
{
    public class CommentControllerTests
    {
        private readonly ICommentRepository _commentRepo;
        //SUT
        private readonly CommentController _commentController;

        private readonly List<Comment> _comments;

        private readonly Comment _comment;

        public CommentControllerTests()
        {
            _commentRepo = A.Fake<ICommentRepository>();
            _commentController = new CommentController(_commentRepo);
            var user = A.Fake<AppUser>();
            _comment = new Comment() { Id = 1, StockId = 1, Title = "Hello", Content = "Desc", CreatedAt = DateTime.Now, AppUserId = "1", CreatedBy = user };
            _comments = new List<Comment>() { _comment };
        }
        [Fact]
        public async Task CommentController_GetAll_ReturnsSuccess()
        {
            //Arrange
            A.CallTo(() => _commentRepo.GetAllAsync()).Returns(_comments);
            //Act
            var result = await _commentController.GetAll();
            //Assert
            result.ShouldNotBeNull();
            var okResult = result.ShouldBeOfType<OkObjectResult>();
            var dto = okResult.Value.ShouldBeOfType<List<Comment>>();
            dto[0].Title.ShouldBe("Hello");
            dto[0].CreatedAt.ShouldBeLessThan(DateTime.Now);
        }

        [Fact]
        public async Task CommentController_GetbyId_ReturnsSuccess()
        {
            //Arrange
            int id = 1;
            A.CallTo(() => _commentRepo.GetByIdAsync(id)).Returns(_comment);
            //Act
            var result = await _commentController.GetbyId(id);
            //Assert
            result.ShouldNotBeNull();
            var okResult = result.ShouldBeOfType<OkObjectResult>();
            var dto = okResult.Value.ShouldBeOfType<CommentDto>();

            dto.Content.ShouldBe("Desc");
        }

        [Fact]
        public async Task CommentController_GetbyId_ReturnsNotFound_OnNullResponse()
        {
            //Arrange
            int id = 99;
            A.CallTo(() => _commentRepo.GetByIdAsync(id)).Returns((Comment)null);
            //Act
            var result = await _commentController.GetbyId(id);
            //Assert
            result.ShouldNotBeNull();
            result.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task CommentController_Create_ReturnsSuccess()
        {
            //Arrange
            var commentCreateRequest = A.Fake<CommentCreateRequest>();
            int stockId = 1;
            A.CallTo(() => _commentRepo.CreateAsync(stockId, commentCreateRequest)).Returns(_comment);
            //Act
            var result = await _commentController.Create(stockId, commentCreateRequest);
            //Assert
            result.ShouldNotBeNull();
            var createdAtActionResult = result.ShouldBeOfType<CreatedAtActionResult>();

            createdAtActionResult.Value.ShouldBeOfType<CommentDto>();
        }

        [Fact]
        public async Task CommentController_Create_ReturnsBadRequest_WhenCreationFails()
        {
            // Arrange
            var request = A.Fake<CommentCreateRequest>();
            int stockId = 1;
            A.CallTo(() => _commentRepo.CreateAsync(stockId, request)).Returns((Comment)null);

            // Act
            var result = await _commentController.Create(stockId, request);

            // Assert
            result.ShouldBeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task CommentController_Update_ReturnsSuccess()
        {
            //Arrange
            var commentUpdateRequest = A.Fake<CommentUpdateRequest>();
            int id = 1;
            A.CallTo(() => _commentRepo.UpdateAsync(id, commentUpdateRequest)).Returns(_comment);
            //Act
            var result = await _commentController.Update(id, commentUpdateRequest);
            //Assert
            result.ShouldNotBeNull();
            var okResult = result.ShouldBeOfType<OkObjectResult>();
            okResult.Value.ShouldBeOfType<CommentDto>();
        }

        [Fact]
        public async Task CommentController_Update_ReturnsNotFound_OnNullResponse()
        {
            var request = A.Fake<CommentUpdateRequest>();
            A.CallTo(() => _commentRepo.UpdateAsync(1, request)).Returns((Comment)null);

            var result = await _commentController.Update(1, request);

            result.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task CommentController_Delete_ReturnsSuccess()
        {
            //Arrange
            int id = 1;
            A.CallTo(() => _commentRepo.DeleteAsync(id)).Returns(_comment);
            //Act
            var result = await _commentController.Delete(id);
            //Assert
            result.ShouldNotBeNull();
            var okResult = result.ShouldBeOfType<OkObjectResult>();
            okResult.Value.ShouldBeOfType<Comment>();
        }

        [Fact]
        public async Task CommentController_Delete_ReturnsNotFound_OnNullResponse()
        {
            A.CallTo(() => _commentRepo.DeleteAsync(1)).Returns((Comment)null);

            var result = await _commentController.Delete(1);

            result.ShouldBeOfType<NotFoundResult>();
        }
    }
}