using BookRental.Common.Enums;
using BookRental.WebAPI.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookRental.WebAPI.Queries
{
    public class GetAllBooksQuery : IRequest<ActionResult<IEnumerable<BookDto>>>
    {
        [FromQuery]
        public BookSortingEnum SortType { get; set; } = BookSortingEnum.Default;

        [FromQuery]
        [Range(1, 500)]
        public int EntriesPerPage { get; set; } = 10;

        [FromQuery]
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;
    }
}
