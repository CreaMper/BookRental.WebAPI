using BookRental.Common.Enums;
using BookRental.EF.Entities;

namespace BookRental.EF.Repositories.Interfaces
{
    public interface IBookRepository : IBaseRepository<BookEntity>
    {
        Task<IEnumerable<BookEntity>> GetAllAsync(int limitTo, int pageNumber, BookSortingEnum sortingEnum);
        Task<bool> IsISBNUnique(string isbn);
    }
}
