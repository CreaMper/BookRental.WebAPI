using BookRental.Common.Enums;
using BookRental.EF.Entities;
using BookRental.EF.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookRental.EF.Repositories
{
    public class BookRepository(MainDbContext dbContext) : IBookRepository
    {
        private readonly MainDbContext _dbContext = dbContext;

        public async Task<IEnumerable<BookEntity>> GetAllAsync()
        {
            return await _dbContext.Book.ToListAsync();
        }

        public async Task<BookEntity?> GetByIdAsync(int id)
        {
            return await _dbContext.Book
                .Include(x => x.Status)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<BookEntity>> GetAllAsync(int limitTo, int pageNumber, BookSortingEnum sortingEnum)
        {
            var query = _dbContext.Book
                .Include(x => x.Status)
                .AsQueryable();

            switch (sortingEnum)
            {
                case BookSortingEnum.TitleAsc:
                    query = query.OrderBy(b => b.Title);
                    break;

                case BookSortingEnum.TitleDesc:
                    query = query.OrderByDescending(b => b.Title);
                    break;

                case BookSortingEnum.AuthorAsc:
                    query = query.OrderBy(b => b.Author);
                    break;

                case BookSortingEnum.AuthorDesc:
                    query = query.OrderByDescending(b => b.Author);
                    break;

                case BookSortingEnum.ISBNAsc:
                    query = query.OrderBy(b => b.ISBN);
                    break;

                case BookSortingEnum.ISBNDesc:
                    query = query.OrderByDescending(b => b.ISBN);
                    break;

                case BookSortingEnum.StatusAsc:
                    query = query.OrderBy(b => b.Status.Name);
                    break;

                case BookSortingEnum.StatusDesc:
                    query = query.OrderByDescending(b => b.Status.Name);
                    break;

                case BookSortingEnum.Default:
                default:
                    query = query.OrderBy(b => b.Id);
                    break;
            }

            return await query
                .Skip((pageNumber - 1) * limitTo)
                .Take(limitTo)
                .ToListAsync();
        }

        public async Task<bool> IsISBNUnique(string isbn)
        {
            return !await _dbContext.Book.AnyAsync(b => b.ISBN == isbn);
        }

        public async Task AddAsync(BookEntity entity)
        {
            await _dbContext.Book.AddAsync(entity);
        }

        public void Update(BookEntity entity)
        {
            _dbContext.Book.Update(entity);
        }

        public void Delete(BookEntity entity)
        {
            _dbContext.Book.Remove(entity);
        }
    }
}
