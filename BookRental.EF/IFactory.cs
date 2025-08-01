using BookRental.EF.Repositories.Interfaces;

namespace BookRental.EF
{
    public interface IFactory : IDisposable
    {
        IBookRepository BookRepository { get; }
        Task SaveChangesAsync();
    }
}
