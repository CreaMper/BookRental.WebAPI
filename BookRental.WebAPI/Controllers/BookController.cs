using BookRental.Common.Enums;
using BookRental.WebAPI.Commands;
using BookRental.WebAPI.Dto;
using BookRental.WebAPI.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookRental.WebAPI.Controllers
{
    [ApiController]
    [Route("book")]
    public class BookController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Get a list of all available books
        /// </summary>
        /// <param name="bookId">Book identifier</param>
        /// <returns>List of available books</returns>
        [HttpGet("{bookId}")]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookDto>> GetBookById([FromRoute] int bookId)
        {
            var query = new GetBookByIdQuery()
            {
                BookId = bookId
            };

            return await _mediator.Send(query);
        }

        /// <summary>
        /// Create a new book
        /// </summary>
        /// <param name="command">Basic book data</param>
        /// <returns>Created book data</returns>
        [HttpPost]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookDto>> CreateBook([FromBody] CreateBookCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Updates book data
        /// </summary>
        /// <param name="command">Basic book data</param>
        /// <param name="bookId">Book identifier</param>
        /// <returns>Updated book</returns>
        [HttpPut("{bookId}")]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookDto>> UpdateBook([FromRoute] int bookId, [FromBody] UpdateBookCommand command)
        {
            command.BookId = bookId;
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Updates book status
        /// </summary>
        /// <param name="bookId">Book identifier</param>
        /// <param name="bookStatus">New book status</param>
        /// <returns>Book data with the new status</returns>
        [HttpPut("{bookId}/status")]
        [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BookDto>> UpdateBookStatus([FromRoute] int bookId, [FromQuery][Required] BookStatusEnum bookStatus)
        {
            var command = new UpdateBookStatusCommand()
            {
                BookId = bookId,
                BookStatus = bookStatus
            };

            return await _mediator.Send(command);
        }

        /// <summary>
        /// Get list of all available books
        /// </summary>
        /// <param name="query">Filter parameters</param>
        /// <returns>List of all available books</returns>
        [HttpGet("get-all")]
        [ProducesResponseType(typeof(IEnumerable<BookDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks([FromQuery] GetAllBooksQuery query)
        {
            return await _mediator.Send(query);
        }

        /// <summary>
        /// Removes the book from the database
        /// </summary>
        /// <param name="bookId">Book identifier</param>
        /// <returns>No content</returns>
        [HttpDelete("{bookId}")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Unit>> DeleteBook([FromRoute] int bookId)
        {
            var command = new DeleteBookCommand()
            {
                BookId = bookId
            };

            return await _mediator.Send(command);
        }
    }
}
