using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using FakeItEasy;
using Xunit;

namespace api.Tests.Controllers
{
    public class CommentControllerTests
    {
        private ICommentRepository _commentRepo;

        public CommentControllerTests()
        {
            _commentRepo = A.Fake<ICommentRepository> fakeCommentRepo();
        }
        [Fact]
        public void CommentController_GetAll_ReturnsSuccess()
        {
            //Arrange

            //Act
            //Assert
        }
    }
}