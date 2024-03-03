using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using MVCExample.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            this.dbSet=_context.Set<T>();
            _context.Products.Include(u => u.Category).Include(u=>u.CategoryId);
        }
        public void Add(T entity)
        {
           dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includes = null)
        {
            IQueryable<T> query = dbSet;
            query=query.Where(filter);
            if (!string.IsNullOrEmpty(includes))
            {
                foreach (var item in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includes=null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includes))
            {
                foreach (var item in includes.Split(new char[] { ',' },StringSplitOptions.RemoveEmptyEntries))
                {
                    query=query.Include(item);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
