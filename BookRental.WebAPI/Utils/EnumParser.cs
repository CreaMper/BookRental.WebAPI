using BookRental.Common.Enums;

namespace BookRental.WebAPI.Utils
{
    public static class EnumParser
    {
        public static BookStatusEnum ParseBookStatus(string status)
        {
            if (Enum.TryParse(status, ignoreCase: true, out BookStatusEnum result))
                return result;

            throw new ArgumentException($"Invalid book status {status}");
        }
    }
}
