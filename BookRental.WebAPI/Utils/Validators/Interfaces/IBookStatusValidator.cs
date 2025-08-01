using BookRental.Common.Enums;

namespace BookRental.WebAPI.Utils.Validators.Interfaces
{
    public interface IBookStatusValidator
    {
        string ValidateStatusUpdate(BookStatusEnum currentStatus, BookStatusEnum newStatus);
    }
}