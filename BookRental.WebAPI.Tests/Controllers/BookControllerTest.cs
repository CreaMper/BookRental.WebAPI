using BookRental.Common.Enums;
using BookRental.WebAPI.Commands;
using BookRental.WebAPI.Controllers;
using BookRental.WebAPI.Dto;
using BookRental.WebAPI.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.MSTest.TestAdapter.ObjectModel;
using Moq;

namespace BookRental.WebAPI.Tests.Controllers
{
    [TestClass]
    public class BookControllerTest
    {
        private Mock<IMediator> _mediatorMock = new();
        private BookController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new BookController(_mediatorMock.Object);
        }

        [TestMethod]
        public async Task GetBookById_Should_ReturnOK()
        {
            var expectedBook = new BookDto 
            { 
                Id = It.IsAny<int>()
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetBookByIdQuery>(), default))
                .ReturnsAsync(new OkObjectResult(expectedBook));

            var result = await _controller.GetBookById(It.IsAny<int>());

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            Assert.AreEqual(expectedBook, ((OkObjectResult)result.Result).Value);
        }

        [TestMethod]
        public async Task UpdateBook_Should_ReturnNotFound()
        {
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateBookCommand>(), default))
                .ReturnsAsync(new NotFoundResult());

            var result = await _controller.UpdateBook(It.IsAny<int>(), new UpdateBookCommand());
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task UpdateBookStatus_Should_ReturnBadRequest()
        {
            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateBookStatusCommand>(), default))
                .ReturnsAsync(new BadRequestObjectResult(string.Empty));

            var result = await _controller.UpdateBookStatus(It.IsAny<int>(), BookStatusEnum.Damaged);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }
    }
}
