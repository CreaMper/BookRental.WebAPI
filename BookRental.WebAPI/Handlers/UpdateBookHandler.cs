using BookRental.EF;
using BookRental.WebAPI.Commands;
using BookRental.WebAPI.Converters.Interfaces;
using BookRental.WebAPI.Dto;
using BookRental.WebAPI.Utils;
using BookRental.WebAPI.Utils.Validators;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.WebAPI.Handlers
{
    public class UpdateBookHandler(IFactory factory, IBookConverter bookConverter) : IRequestHandler<UpdateBookCommand, ActionResult<BookDto>>
    {
        private readonly IFactory _factory = factory;
        private readonly IBookConverter _bookConverter = bookConverter;

        public async Task<ActionResult<BookDto>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _factory.BookRepository.GetByIdAsync(request.BookId);
            if (book == null)
                return APIResult.NotFound($"Book with Id `{request.BookId}` does not exists!");

            if (!string.IsNullOrEmpty(request.ISBN) && !string.Equals(request.ISBN, book.ISBN))
            {
                if (!ISBNValidator.IsValid(request.ISBN))
                    return APIResult.BadRequest("ISBN checksum validation failed!");

                if (!await _factory.BookRepository.IsISBNUnique(request.ISBN))
                    return APIResult.BadRequest($"Book with ISBN `{request.ISBN}` already exists!");

                book.ISBN = request.ISBN;
            }

            if (!string.IsNullOrEmpty(request.Author))
                book.Author = request.Author;

            if (!string.IsNullOrEmpty(request.Title))
                book.Title = request.Title;

            await _factory.SaveChangesAsync();

            return APIResult.Ok(_bookConverter.Convert(book));
        }
    }
}
