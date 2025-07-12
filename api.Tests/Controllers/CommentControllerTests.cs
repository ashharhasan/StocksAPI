using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers;
using api.Dtos.Comment;
using api.Interfaces;
using api.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace api.Tests.Controllers
{
    public class CommentControllerTests
    {
        private readonly ICommentRepository commentRepo;
        //SUT
        private readonly CommentController commentController;

        public CommentControllerTests()
        {
            commentRepo = A.Fake<ICommentRepository>();
            commentController = A.Fake<CommentController>();
        }
        [Fact]
        public void CommentController_GetAll_ReturnsSuccess()
        {
            //Arrange
            var comments = A.Fake<List<Comment>>();
            A.CallTo(() => commentRepo.GetAllAsync()).Returns(comments);
            //Act
            var result = commentController.GetAll();
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void CommentController_GetbyId_ReturnsSuccess()
        {
            //Arrange
            var comment = A.Fake<Comment>();
            int id = 1;
            A.CallTo(() => commentRepo.GetByIdAsync(id)).Returns(comment);
            //Act
            var result = commentController.GetbyId(id);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void CommentController_Create_ReturnsSuccess()
        {
            //Arrange
            var comment = A.Fake<Comment>();
            var commentCreateRequest = A.Fake<CommentCreateRequest>();
            int stockId = 1;
            A.CallTo(() => commentRepo.CreateAsync(stockId, commentCreateRequest)).Returns(comment);
            //Act
            var result = commentController.Create(stockId, commentCreateRequest);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();
        }

        [Fact]
        public void CommentController_Update_ReturnsSuccess()
        {
            //Arrange
            var comment = A.Fake<Comment>();
            var commentUpdateRequest = A.Fake<CommentUpdateRequest>();
            int id = 1;
            A.CallTo(() => commentRepo.UpdateAsync(id, commentUpdateRequest)).Returns(comment);
            //Act
            var result = commentController.Update(id, commentUpdateRequest);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();
        }
        
        [Fact]
        public void CommentController_Delete_ReturnsSuccess()
        {
            //Arrange
            var comment = A.Fake<Comment>();
            int id = 1;
            A.CallTo(() => commentRepo.DeleteAsync(id)).Returns(comment);
            //Act
            var result = commentController.Delete(id);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();
        }
    }
}