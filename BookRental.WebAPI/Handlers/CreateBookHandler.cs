using BookRental.Common.Enums;
using BookRental.EF;
using BookRental.EF.Entities;
using BookRental.WebAPI.Commands;
using BookRental.WebAPI.Converters.Interfaces;
using BookRental.WebAPI.Dto;
using BookRental.WebAPI.Utils;
using BookRental.WebAPI.Utils.Validators;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.WebAPI.Handlers
{
    public class CreateBookHandler(IFactory factory, IBookConverter bookConverter) : IRequestHandler<CreateBookCommand, ActionResult<BookDto>>
    {
        private readonly IFactory _factory = factory;
        private readonly IBookConverter _bookConverter = bookConverter;

        public async Task<ActionResult<BookDto>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            if(!ISBNValidator.IsValid(request.ISBN))
                return APIResult.BadRequest("ISBN checksum validation failed!");

            if (!await _factory.BookRepository.IsISBNUnique(request.ISBN))
                return APIResult.BadRequest($"Book with ISBN `{request.ISBN}` already exists!");

            var book = new BookEntity()
            {
                Author = request.Author,
                Title = request.Title,
                ISBN = request.ISBN,
                Status = new StatusEntity()
                {
                    Name = BookStatusEnum.Available.ToString()
                }
            };

            await _factory.BookRepository.AddAsync(book);
            await _factory.SaveChangesAsync();

            return APIResult.Ok(_bookConverter.Convert(book));
        }
    }
}
