using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AuthenticationServer.WebApi.Repository
{
    public interface IRepository<TEntitiy> where TEntitiy : class
    {
         TEntitiy Get(int id);
         IEnumerable<TEntitiy> GetAll();
         IEnumerable<TEntitiy> Find(Expression<Func<TEntitiy, bool>> predicate);

         void Add(TEntitiy entity);
         void AddRange(IEnumerable<TEntitiy> entities);

        void Remove(TEntitiy entity);
        void RemoveRange(IEnumerable<TEntitiy> entities);
    }
}