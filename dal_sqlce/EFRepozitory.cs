using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using dal_sqlce.Context;

namespace DomainClasses
{
    public class EFRepozitory<T> : IRepozitory<T>
        where T : class
    {
        egzEntities context = null;

        public DbContext Context
        {
            get
            {
                if (context == null)
                    context = new egzEntities();
                return context;
            }

            set
            {
                context = value as egzEntities;
            }
        }

        public IRepozitory<TNested> GetRepozitory<TNested>() where TNested : class
        {
            IRepozitory<TNested> rep = new EFRepozitory<TNested>(Context);
            return rep;
        }

        public EFRepozitory()
        {
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public EFRepozitory(DbContext _context)
        {
            Context = _context;
            Context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }


        public bool Delete(T delT)
        {
            Context.Set<T>().Remove(delT);
            return Context.SaveChanges() > 0;
        }

        public bool InsertOrUpdate(T editT)
        {
            if (Context.Entry(editT).State == EntityState.Detached)
                Context.Set<T>().Add(editT);
            else
            {
                Context.Entry(editT).State = System.Data.Entity.EntityState.Modified;
            }
            return Context.SaveChanges() > 0;
        }

        public T Find(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public IQueryable<T> GetList()
        {
            return Context.Set<T>();
        }

        public IQueryable<T> GetListWhere(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetListWhere(IEnumerable<string> includes, System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var ctx = Context.Set<T>();
            foreach (var item in includes)
            {
                ctx.Include(item);
            }            
            return ctx.Where(predicate);
        }

        public IQueryable<T> GetList(params System.Linq.Expressions.Expression<Func<T, object>>[] includes)
        {
            var ctx = Context.Set<T>();
            IQueryable<T> query = null;
            foreach (var item in includes)
            {
                if (query == null)
                    query = ctx.Include(item);
                else
                    query = query.Include(item);
            }   
            return query;
        }
    }
}
