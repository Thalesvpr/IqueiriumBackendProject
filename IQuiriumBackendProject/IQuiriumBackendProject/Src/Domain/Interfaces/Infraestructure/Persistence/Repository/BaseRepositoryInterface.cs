namespace IqueiriumBackendProject.Src.Domain.Interfaces.Infraestructure.Persistence.Repository
{
    public interface BaseRepositoryInterface<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        IQueryable<T> Query();
        Task<IEnumerable<T>> GetPagedAsync(int pageNumber, int pageSize);
    }
}

