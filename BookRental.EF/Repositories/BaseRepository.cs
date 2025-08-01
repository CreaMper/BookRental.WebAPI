namespace BookRental.EF.Repositories
{
    public interface IBaseRepository<T>
    {
        public Task<T?> GetByIdAsync(int id);
        public Task<IEnumerable<T>> GetAllAsync();
        public Task AddAsync(T entity);
        public void Update(T entity);
        public void Delete(T entity);
    }
}
