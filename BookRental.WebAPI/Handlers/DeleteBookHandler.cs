using BookRental.EF;
using BookRental.WebAPI.Commands;
using BookRental.WebAPI.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.WebAPI.Handlers
{
    public class DeleteBookHandler(IFactory factory) : IRequestHandler<DeleteBookCommand, ActionResult<Unit>>
    {
        private readonly IFactory _factory = factory;

        public async Task<ActionResult<Unit>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _factory.BookRepository.GetByIdAsync(request.BookId);
            if (book == null)
                return APIResult.NotFound("Book not found");
        
            _factory.BookRepository.Delete(book);
            await _factory.SaveChangesAsync();

            return APIResult.NoContent();
        }
    }
}
