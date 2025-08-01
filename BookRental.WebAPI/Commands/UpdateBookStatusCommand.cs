using BookRental.Common.Enums;
using BookRental.WebAPI.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookRental.WebAPI.Commands
{
    public class UpdateBookStatusCommand : IRequest<ActionResult<BookDto>>
    {
        public int BookId { get; set; }
        public BookStatusEnum BookStatus { get; set; }
    }
}
