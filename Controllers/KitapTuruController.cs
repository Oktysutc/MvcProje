using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MvcProje.Models;
using MvcProje.Utility;

namespace MvcProje.Controllers
{
    public class KitapTuruController : Controller
    {
        private readonly UygulamaDbContext _uygulamaDbContext;
        public KitapTuruController(UygulamaDbContext context)
        {
            _uygulamaDbContext = context;
        }
        public IActionResult Index()
        {
            List<KitapTuru> objKitapTuruList = _uygulamaDbContext.KitapTurleri.ToList();
            
            return View(objKitapTuruList);
        }
        // new kitapturu add action page
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(KitapTuru kitapTuru)
        {
            if (ModelState.IsValid)
            {
                _uygulamaDbContext.KitapTurleri.Add(kitapTuru);
                _uygulamaDbContext.SaveChanges();//savechanges bilgiler veritabanına eklenmez
                return RedirectToAction("Index", "KitapTuru");
            }
            return View();
        }
        public IActionResult Guncelle(int? id)
        {if(id==null || id == 0)
            {
                return NotFound();
            }
            KitapTuru kitapTuruVt = _uygulamaDbContext.KitapTurleri.Find(id);
            return View();
        }
        [HttpPost]
        public IActionResult Guncelle(KitapTuru kitapTuru)
        {
            if (ModelState.IsValid)
            {
                _uygulamaDbContext.KitapTurleri.Add(kitapTuru);
                _uygulamaDbContext.SaveChanges();//savechanges bilgiler veritabanına eklenmez
                return RedirectToAction("Index", "KitapTuru");
            }
            return View();
        }


    }
}
