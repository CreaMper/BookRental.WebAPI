using BookRental.WebAPI.Dto;
using BookRental.WebAPI.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookRental.WebAPI.Commands
{
    public class CreateBookCommand : IRequest<ActionResult<BookDto>>
    {
        [FromBody]
        [MaxLength(99)]
        public required string Title { get; set; }

        [FromBody]
        [MaxLength(99)]
        public required string Author { get; set; }

        [FromBody]
        [ValidateRegex(RegexValidationType.ISBN)]
        public required string ISBN { get; set; }
    }
}
