using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DomainClasses
{
    public interface IRepozitory<T>
    {
        DbContext Context { get; set; }
        IRepozitory<TNested> GetRepozitory<TNested>() where TNested : class;
        T Find(int id);
        IQueryable<T> GetList();
        IQueryable<T> GetList(params System.Linq.Expressions.Expression<Func<T, object>>[] includes);
        IQueryable<T> GetListWhere(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetListWhere(IEnumerable<string> includes, Expression<Func<T, bool>> predicate);
        bool InsertOrUpdate(T newT);
        bool Delete(T delT);
    }
}
