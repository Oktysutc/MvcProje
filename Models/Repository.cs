using Microsoft.EntityFrameworkCore;
using MvcProje.Utility;
using System.Linq.Expressions;

namespace MvcProje.Models
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly UygulamaDbContext _uygulamaDbContext;
        internal DbSet<T> dbSet;// db.set=uygulamadbcontext.kitapturleri
        public Repository(UygulamaDbContext uygulamaDbContext)
        {
            _uygulamaDbContext= uygulamaDbContext;
            this.dbSet = _uygulamaDbContext.Set<T>();
        }
       

        public void Ekle(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filtre)
        {
            IQueryable<T> sorgu = dbSet;
            sorgu = sorgu.Where(filtre);
            return sorgu.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> sorgu = dbSet;
            return sorgu.ToList();
        }

        public void Sil(T entity)
        {
            dbSet.Remove(entity);

        }

        public void SilAralık(IEnumerable<T> entities)
        {
         dbSet.RemoveRange(entities);
        }
    }
}
