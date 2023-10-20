using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MvcProje.Models;
using MvcProje.Utility;

namespace MvcProje.Controllers
{
    public class KitapController:Controller
    {
        private readonly IKitapRepository _kitapRepository;
        public KitapController(IKitapRepository context)
        {
            _kitapRepository = context;
        }
        public IActionResult Index()
        {
            List<Kitap> objKitapList = _kitapRepository.GetAll().ToList();

            return View(objKitapList);
        }
        // new kitap add action page
        public IActionResult Ekle()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                _kitapRepository.Ekle(kitap);
                _kitapRepository.Kaydet();//savechanges bilgiler veritabanına eklenmez
                TempData["basarili"] = "yeni kitap  başarıyla oluşturuldu";
                return RedirectToAction("Index", "Kitap");
            }
            return View();
        }
        public IActionResult Guncelle(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Kitap? kitapVt = _kitapRepository.Get(u=>u.Id==id);
            if (kitapVt == null)
            {
                return NotFound();
            }
            return View(kitapVt);
        }
        [HttpPost]
        public IActionResult Guncelle(Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                _kitapRepository.Guncelle(kitap);
                _kitapRepository.Kaydet();//savechanges bilgiler veritabanına eklenmez
                TempData["basarili"] = "yeni kitap  başarıyla güncellendi";
                return RedirectToAction("Index", "Kitap");
            }
            return View();
        }
        // get action 
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Kitap? kitapVt = _kitapRepository.Get(u => u.Id == id);
            if (kitapVt == null)
            {
                return NotFound();
            }
            return View(kitapVt);
        }
        [HttpPost,ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            Kitap? kitap = _kitapRepository.Get(u => u.Id == id);
            if (kitap == null)
            {
                return NotFound();
            }
            _kitapRepository.Sil(kitap);
            _kitapRepository.Kaydet();
            TempData["basarili"] = "yeni kitap başarıyla silindi";
            return RedirectToAction("Index", "Kitap");

        }


    }
}
