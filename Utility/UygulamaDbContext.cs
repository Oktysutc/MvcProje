using Microsoft.EntityFrameworkCore;
using MvcProje.Models;

namespace MvcProje.Utility
{
    public class UygulamaDbContext:DbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext>options): base(options){ }
        public DbSet<KitapTuru> KitapTurleri { get; set; }
        

        public DbSet<Kitap> Kitaplar { get; set; }
    }
}
