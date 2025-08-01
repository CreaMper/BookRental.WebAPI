using BookRental.WebAPI.Dto;
using BookRental.WebAPI.Utils;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BookRental.WebAPI.Commands
{
    public class UpdateBookCommand : IRequest<ActionResult<BookDto>>
    {   
        [JsonIgnore]
        public int BookId { get; set; }

        [FromBody]
        [MaxLength(99)]
        public string Title { get; set; }

        [FromBody]
        [MaxLength(99)]
        public string Author { get; set; }

        [FromBody]
        [ValidateRegex(RegexValidationType.ISBN)]
        public string ISBN { get; set; }
    }
}
