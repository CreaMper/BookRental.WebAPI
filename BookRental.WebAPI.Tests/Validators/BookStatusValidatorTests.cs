using BookRental.Common.Enums;
using BookRental.WebAPI.Utils.Validators;

namespace BookRental.WebAPI.Tests.Validators
{
    [TestClass]
    public class BookStatusValidatorTests
    {
        private readonly BookStatusValidator _sut = new();

        [TestMethod]
        public void ValidateStatusUpdate_Should_ReturnEmptyString_IfSucess()
        {
            var result = _sut.ValidateStatusUpdate(BookStatusEnum.Available, BookStatusEnum.Borrowed);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void ValidateStatusUpdate_Should_ReturnString_IfInvalidTransfer()
        {
            var result = _sut.ValidateStatusUpdate(BookStatusEnum.Available, BookStatusEnum.Returned);
            Assert.AreNotEqual(string.Empty, result);
        }

        [TestMethod]
        public void ValidateStatusUpdate_Should_ReturnEmptyString_IfTransferedFromBorrowedToReturned()
        {
            var result = _sut.ValidateStatusUpdate(BookStatusEnum.Borrowed, BookStatusEnum.Returned);
            Assert.AreEqual(string.Empty, result);
        }
    }
}
