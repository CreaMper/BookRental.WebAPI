using BookRental.EF.Entities;
using BookRental.EF.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookRental.EF.Repositories
{
    public class StatusRepository(MainDbContext dbContext) : IStatusRepository
    {
        private readonly MainDbContext _dbContext = dbContext;

        public async Task<IEnumerable<StatusEntity>> GetAllAsync()
        {
            return await _dbContext.Status.ToListAsync();
        }

        public async Task<StatusEntity?> GetByIdAsync(int id)
        {
            return await _dbContext.Status.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(StatusEntity entity)
        {
            await _dbContext.Status.AddAsync(entity);
        }

        public void Update(StatusEntity entity)
        {
            _dbContext.Status.Update(entity);
        }

        public void Delete(StatusEntity entity)
        {
            _dbContext.Status.Remove(entity);
        }
    }
}
