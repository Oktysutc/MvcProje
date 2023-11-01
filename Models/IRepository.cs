using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace MvcProje.Models
{
    public interface IRepository<T> where T : class
    {
        //T->kitapturu
        IEnumerable<T> GetAll(string? includeProps = null);
        T Get(Expression<Func<T, bool>> filtre, string? includeProps = null);
        void Ekle(T entity);
        void Sil(T entity);
        void SilAralık(IEnumerable<T> entities);
    }
}
