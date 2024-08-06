using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Helper;

namespace WorkoutPlanner.Service;

public abstract class BaseService<T> where T : class
{
    protected readonly DatabaseContext Db;
    protected readonly DbSet<T> DbSet;
    protected readonly IMapper Mapper;

    protected BaseService(DatabaseContext databaseContext, IMapper mapper)
    {
        Db = databaseContext;
        DbSet = Db.Set<T>();
        Mapper = mapper;
    }
    
    protected async Task<T> CreateAsync(T entity)
    {
        var entry = await DbSet.AddAsync(entity);
        await SaveAsync();
        return entry.Entity;
    }

    protected async Task RemoveAsync(T entity)
    {
        DbSet.Remove(entity);
        await SaveAsync();
    }


    protected IQueryable<T> GetQueryable(bool tracked = true)
    {
        IQueryable<T> query = DbSet;

        if (!tracked)
        {
            query = query.AsNoTracking();
        }

        return query;
    }
    
    protected async Task SaveAsync()
    {
        await Db.SaveChangesAsync();
    }
}