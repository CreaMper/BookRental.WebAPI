using BookRental.WebAPI.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.WebAPI.Queries
{
    public class GetBookByIdQuery : IRequest<ActionResult<BookDto>>
    {
        [FromRoute]
        public int BookId { get; set; }
    }
}
