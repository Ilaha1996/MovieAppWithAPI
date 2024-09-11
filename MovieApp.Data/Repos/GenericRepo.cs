using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Entities;
using MovieApp.Core.Repos;
using MovieApp.Data.DAL;
using System.Linq.Expressions;

namespace MovieApp.Data.Repos;

public class GenericRepo<TEntity> : IGenericRepo<TEntity> where TEntity : BaseEntity, new()
{
    private readonly AppDbContext _context;

    public GenericRepo(AppDbContext context)
    {
        _context = context;
    }
    public DbSet<TEntity> Table => _context.Set<TEntity>();

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task CreateAsync(TEntity entity)
    {
        await Table.AddAsync(entity);
    }

    public void DeleteAsync(TEntity entity)
    {
        Table.Remove(entity);
    }

    public IQueryable<TEntity> GetByExpressionAsync(Expression<Func<TEntity, bool>>? expression = null, bool asNoTracking = false, params string[] includes)
    {
        var query = Table.AsQueryable();
        if (includes.Length > 0)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        query = asNoTracking == true ? query.AsNoTracking() : query;

        return expression is not null ? query.Where(expression) : query;
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await Table.FindAsync(id);
    }
}
