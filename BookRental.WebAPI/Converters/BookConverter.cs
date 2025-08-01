using BookRental.EF.Entities;
using BookRental.WebAPI.Converters.Interfaces;
using BookRental.WebAPI.Dto;

namespace BookRental.WebAPI.Converters
{
    public class BookConverter : IBookConverter
    {
        public BookDto Convert(BookEntity bookEntity)
        {
            return new BookDto
            {
                Id = bookEntity.Id,
                Title = bookEntity.Title,
                Author = bookEntity.Author,
                ISBN = bookEntity.ISBN,
                Status = bookEntity.Status.Name
            };
        }
    }
}
