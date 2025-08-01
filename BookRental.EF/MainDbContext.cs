using BookRental.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookRental.EF
{
    public class MainDbContext(DbContextOptions<MainDbContext> options) : DbContext(options)
    {
        internal DbSet<BookEntity> Book { get; set; }
        internal DbSet<StatusEntity> Status { get; set; }
    }
}
