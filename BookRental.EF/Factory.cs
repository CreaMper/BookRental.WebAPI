using BookRental.EF.Repositories;
using BookRental.EF.Repositories.Interfaces;

namespace BookRental.EF
{
    public class Factory(MainDbContext dbcontext) : IFactory
    {
        private readonly MainDbContext _dbcontext = dbcontext;

        public IBookRepository BookRepository => new BookRepository(_dbcontext);


        public async Task SaveChangesAsync()
        {
            await _dbcontext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbcontext.Dispose();
        }
    }
}
