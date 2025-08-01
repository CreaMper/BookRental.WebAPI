using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.WebAPI.Commands
{
    public class DeleteBookCommand : IRequest<ActionResult<Unit>>
    {
        public int BookId { get; set; }
    }
}
