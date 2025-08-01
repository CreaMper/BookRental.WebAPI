using BookRental.Common.Enums;
using BookRental.WebAPI.Utils.Validators.Interfaces;

namespace BookRental.WebAPI.Utils.Validators
{
    public class BookStatusValidator : IBookStatusValidator
    {
        private readonly Dictionary<BookStatusEnum, List<BookStatusEnum>> AvailableStatusTransitions = new()
        {
            {
                BookStatusEnum.Available,
                [
                    BookStatusEnum.Borrowed,
                    BookStatusEnum.Damaged
                ]
            },
            {
                BookStatusEnum.Borrowed,
                [
                    BookStatusEnum.Returned,
                    BookStatusEnum.Damaged
                ]
            },
            {
                BookStatusEnum.Returned,
                [
                    BookStatusEnum.Available,
                    BookStatusEnum.Damaged
                ]
            },
            {
                BookStatusEnum.Damaged,
                [
                    BookStatusEnum.Available
                ]
            }
        };

        public string ValidateStatusUpdate(BookStatusEnum currentStatus, BookStatusEnum newStatus)
        {
            if (!AvailableStatusTransitions[currentStatus].Contains(newStatus))
                return $"Validation failed! Invalid status transition from `{currentStatus}` to `{newStatus}`! Allowed transitions: `{string.Join("`, `", AvailableStatusTransitions[currentStatus])}`";

            return string.Empty;
        }
    }
}
