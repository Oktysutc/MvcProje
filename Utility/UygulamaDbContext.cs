using Microsoft.EntityFrameworkCore;
using MvcProje.Models;
// veri tabanında entity framewrok tablosu olusturması için ilgili model sınıflarınızı buraya eklemelisiniz
namespace MvcProje.Utility
{
    public class UygulamaDbContext:DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext>options): base(options){ }
        public DbSet<KitapTuru> KitapTurleri { get; set; }
        

        public DbSet<Kitap> Kitaplar { get; set; }
        public DbSet<Kiralama> Kiralamalar { get; set; }
    }
}
