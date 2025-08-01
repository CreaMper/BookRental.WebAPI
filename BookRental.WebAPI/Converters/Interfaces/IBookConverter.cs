using BookRental.EF.Entities;
using BookRental.WebAPI.Dto;

namespace BookRental.WebAPI.Converters.Interfaces
{
    public interface IBookConverter
    {
        BookDto Convert(BookEntity bookEntity);
    }
}