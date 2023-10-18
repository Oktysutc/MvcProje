using MvcProje.Utility;

namespace MvcProje.Models
{
    public class KitapTuruRepository : Repository<KitapTuru>, IKitapTuruRepository
    {
        private  UygulamaDbContext _uygulamaDbContext;
        public KitapTuruRepository(UygulamaDbContext uygulamaDbContext) : base(uygulamaDbContext)
        {
        }

        public void Guncelle(KitapTuru kitapTuru)
        {
            _uygulamaDbContext.Update(kitapTuru);
        }

        public void Kaydet()
        {
         _uygulamaDbContext.SaveChanges();
        }
    }
}
