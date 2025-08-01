using Microsoft.EntityFrameworkCore;

namespace BookRental.EF.Entities
{
    [Index(nameof(ISBN), IsUnique = true)]
    public class BookEntity : BaseEntity
    {
        public required string Title { get; set; }
        public required string Author { get; set; }
        public required string ISBN { get; set; }
        public required StatusEntity Status { get; set; }
    }
}
