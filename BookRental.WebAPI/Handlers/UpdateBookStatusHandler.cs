using BookRental.EF;
using BookRental.EF.Entities;
using BookRental.WebAPI.Commands;
using BookRental.WebAPI.Converters.Interfaces;
using BookRental.WebAPI.Dto;
using BookRental.WebAPI.Utils;
using BookRental.WebAPI.Utils.Validators.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.WebAPI.Handlers
{
    public class UpdateBookStatusHandler(IFactory factory, IBookConverter bookConverter, IBookStatusValidator bookStatusValidator) : IRequestHandler<UpdateBookStatusCommand, ActionResult<BookDto>>
    {
        private readonly IBookStatusValidator _bookStatusValidator = bookStatusValidator;
        private readonly IFactory _factory = factory;
        private readonly IBookConverter _bookConverter = bookConverter;

        public async Task<ActionResult<BookDto>> Handle(UpdateBookStatusCommand request, CancellationToken cancellationToken)
        {
            var book = await _factory.BookRepository.GetByIdAsync(request.BookId);
            if (book == null)
                return APIResult.NotFound($"Book with Id `{request.BookId}` does not exist!");

            var currentStatus = EnumParser.ParseBookStatus(book.Status.Name);
            if (currentStatus.Equals(request.BookStatus))
                return APIResult.BadRequest($"This book already have the requested status!");

            var result = _bookStatusValidator.ValidateStatusUpdate(currentStatus, request.BookStatus);
            if (!string.IsNullOrEmpty(result))
                return APIResult.BadRequest($"{result}");

            book.Status = new StatusEntity()
            {
                Name = request.BookStatus.ToString()
            };

            _factory.BookRepository.Update(book);
            await _factory.SaveChangesAsync();

            return APIResult.Ok(_bookConverter.Convert(book));
        }
    }
}
