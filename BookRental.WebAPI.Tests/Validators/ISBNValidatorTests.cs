using BookRental.WebAPI.Utils.Validators;

namespace BookRental.WebAPI.Tests.Validators
{
    [TestClass]
    public class ISBNValidatorTests
    {
        [TestMethod]
        [DataRow("103243791X")]
        [DataRow("3196648907")]
        [DataRow("7349286481")]
        public void IsValid_Should_ReturnTrue_ForValidISBN10(string isbn)
        {
            Assert.IsTrue(ISBNValidator.IsValid(isbn));
        }

        [TestMethod]
        [DataRow("103213791X")]
        [DataRow("3126648917")]
        [DataRow("734128642X")]
        public void IsValid_Should_ReturnFalse_ForInvalidISBN10(string isbn)
        {
            Assert.IsFalse(ISBNValidator.IsValid(isbn));
        }

        [TestMethod]
        [DataRow("9780271494289")]
        [DataRow("9784855594020")]
        [DataRow("9782638058738")]
        public void IsValid_Should_ReturnTrue_ForValidISBN13(string isbn)
        {
            Assert.IsTrue(ISBNValidator.IsValid(isbn));
        }

        [TestMethod]
        [DataRow("9187484370454")]
        [DataRow("foo -// =sda@ 21")]
        [DataRow("9786583606212")]
        public void IsValid_Should_ReturnFalse_ForInvalidISBN13(string isbn)
        {
            Assert.IsFalse(ISBNValidator.IsValid(isbn));
        }
    }
}
