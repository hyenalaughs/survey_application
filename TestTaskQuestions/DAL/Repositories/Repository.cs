using Microsoft.EntityFrameworkCore;
using TestTaskQuestions.DAL.Core;
using TestTaskQuestions.DAL.Interfaces;

namespace TestTaskQuestions.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly SurveyDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(SurveyDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id) =>
            await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> GetAllAsync() =>
            await _dbSet.ToListAsync();

        public async Task AddAsync(T entity) =>
            await _dbSet.AddAsync(entity);

        public void Update(T entity) =>
            _dbSet.Update(entity);

        public void Delete(T entity) =>
            _dbSet.Remove(entity);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
