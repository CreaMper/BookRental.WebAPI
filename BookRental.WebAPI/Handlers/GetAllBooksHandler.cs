using BookRental.EF;
using BookRental.WebAPI.Converters.Interfaces;
using BookRental.WebAPI.Dto;
using BookRental.WebAPI.Queries;
using BookRental.WebAPI.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.WebAPI.Handlers
{
    public class GetAllBooksHandler(IFactory factory, IBookConverter bookConverter) : IRequestHandler<GetAllBooksQuery, ActionResult<IEnumerable<BookDto>>>
    {
        public readonly IFactory _factory = factory;
        public readonly IBookConverter _bookConverter = bookConverter;

        public async Task<ActionResult<IEnumerable<BookDto>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _factory.BookRepository.GetAllAsync(request.EntriesPerPage, request.PageNumber, request.SortType);

            return APIResult.Ok(books.Select(x => _bookConverter.Convert(x)));
        }
    }
}
