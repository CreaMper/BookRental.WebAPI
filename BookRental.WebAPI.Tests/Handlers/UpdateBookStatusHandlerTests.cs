using BookRental.Common.Enums;
using BookRental.EF.Entities;
using BookRental.EF;
using BookRental.WebAPI.Commands;
using BookRental.WebAPI.Converters.Interfaces;
using BookRental.WebAPI.Dto;
using BookRental.WebAPI.Handlers;
using BookRental.WebAPI.Utils.Validators.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookRental.WebAPI.Tests.Handlers
{
    [TestClass]
    public class UpdateBookStatusHandlerTests
    {
        private Mock<IFactory> _factoryMock;
        private Mock<IBookConverter> _converterMock;
        private Mock<IBookStatusValidator> _validatorMock;
        private UpdateBookStatusHandler _sut;

        private BookEntity _book;
        private UpdateBookStatusCommand _command = new();

        [TestInitialize]
        public void TestInitialize()
        {
            _factoryMock = new Mock<IFactory>();
            _converterMock = new Mock<IBookConverter>();
            _validatorMock = new Mock<IBookStatusValidator>();
            _sut = new UpdateBookStatusHandler(
                _factoryMock.Object,
                _converterMock.Object,
                _validatorMock.Object
            );

            _book = new BookEntity
            {
                Id = 1,
                Author = "Test Author",
                Title = "Test Book",
                ISBN = "1234567890123",
                Status = new StatusEntity
                {
                    Name = BookStatusEnum.Available.ToString()
                }
            };

            _command = new UpdateBookStatusCommand
            {
                BookId = 1,
                BookStatus = BookStatusEnum.Borrowed
            };
        }

        [TestMethod]
        public async Task Handle_Should_ReturnNotFound_WhenBookNotExists()
        {
            _factoryMock.Setup(x => x.BookRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((BookEntity)null);

            var result = await _sut.Handle(_command, It.IsAny<CancellationToken>());

            Assert.IsNotNull(result.Result as NotFoundObjectResult);
        }

        [TestMethod]
        public async Task Handle_Should_ReturnBadRequest_WhenStatusTransitionInvalid()
        {
            _command.BookStatus = BookStatusEnum.Available;

            _factoryMock.Setup(x => x.BookRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_book);

            _validatorMock.Setup(x => x.ValidateStatusUpdate(It.IsAny<BookStatusEnum>(), It.IsAny<BookStatusEnum>()))
                .Returns("");

            var result = await _sut.Handle(_command, It.IsAny<CancellationToken>());

            Assert.IsNotNull(result.Result as BadRequestObjectResult);
        }

        [TestMethod]
        public async Task Handle_Should_ReturnOk_WhenStatusUpdatedSuccessfully()
        {
            _factoryMock.Setup(x => x.BookRepository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_book);

            _validatorMock.Setup(x => x.ValidateStatusUpdate(It.IsAny<BookStatusEnum>(), It.IsAny<BookStatusEnum>()))
                .Returns(string.Empty);

            _converterMock.Setup(x => x.Convert(It.IsAny<BookEntity>()))
                .Returns(new BookDto());

            var result = await _sut.Handle(_command, It.IsAny<CancellationToken>());

            Assert.IsNotNull(result.Result as OkObjectResult);
        }
    }
}