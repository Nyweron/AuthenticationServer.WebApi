using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AuthenticationServer.WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationServer.WebApi.Repository
{
  public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
  {
    protected readonly DbContext Context;

    public Repository(DbContext context)
    {
      Context = context;
    }

    public void Add(TEntity entity)
    {
      Context.Set<TEntity>().Add(entity);
    }

    public async Task AddAsync(TEntity entity)
    {
      await Context.Set<TEntity>().AddAsync(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
      Context.Set<TEntity>().AddRange(entities);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
      await Context.Set<TEntity>().AddRangeAsync(entities);
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
      return Context.Set<TEntity>().Where(predicate);
    }

    public TEntity Get(int id)
    {
      return Context.Set<TEntity>().Find(id);
    }

    public IEnumerable<TEntity> GetAll()
    {
      return Context.Set<TEntity>().ToList();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
      return await Context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetAsync(int id)
    {
       return await Context.Set<TEntity>().FindAsync(id);
    }

    public void Remove(TEntity entity)
    {
      Context.Set<TEntity>().Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
      Context.Set<TEntity>().RemoveRange(entities);
    }
  }
}