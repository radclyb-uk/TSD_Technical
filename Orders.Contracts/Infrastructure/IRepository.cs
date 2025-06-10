namespace Orders.Contracts.Infrastructure
{
    public interface IRepository<T> where T: class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(T entity);
        Task<bool> UpdateAsync(T entity);
    }
}
