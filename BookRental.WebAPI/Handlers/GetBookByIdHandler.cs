using BookRental.EF;
using BookRental.WebAPI.Converters.Interfaces;
using BookRental.WebAPI.Dto;
using BookRental.WebAPI.Queries;
using BookRental.WebAPI.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.WebAPI.Handlers
{
    public class GetBookByIdHandler(IFactory factory, IBookConverter bookConverter) : IRequestHandler<GetBookByIdQuery, ActionResult<BookDto>>
    {
        private readonly IFactory _factory = factory;
        private readonly IBookConverter _bookConverter = bookConverter;

        public async Task<ActionResult<BookDto>> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _factory.BookRepository.GetByIdAsync(request.BookId);
            if (book == null)
                return APIResult.NotFound($"Book with Id `{request.BookId}` does not exists!");

            return APIResult.Ok(_bookConverter.Convert(book));
        }
    }
}
