using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MvcProje.Models;
using MvcProje.Utility;

namespace MvcProje.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]//sadece bunun girmesini sağlar.
    public class KiralamaController:Controller
    {
        private readonly IKiralamaRepository _KiralamaRepository;
        private readonly IKitapRepository _kitapRepository;
 public readonly IWebHostEnvironment _webHostEnvironment;
        // private readonly IKiralamaRepository _kiralamaRepository;
        //private readonly IKitapTuruRepository _kitapTuruRepository;
        //private readonly IKitapTuruRepository kitapTuruRepository;
       

        public KiralamaController(IKiralamaRepository KiralamaRepository, IKitapRepository kitapRepository, IWebHostEnvironment webHostEnvironment)
        {
            _KiralamaRepository = KiralamaRepository; // new kiralama repository !!!!
            _kitapRepository = kitapRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            // List<Kitap> objKitapList = _kitapRepository.GetAll().ToList();
            // IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll(includeProps:"KitapTuru")
            
            List<Kiralama> objKiralamaList = _KiralamaRepository.GetAll(includeProps: "Kitap").ToList();
            return View(objKiralamaList);
            /*
                .Select(k => new SelectListItem
            {
                Text=k.Ad,
                Value=k.Id.ToString()
            });
            */

            //return View(objKitapList);
        }
        // new kitap add action page
        public IActionResult EkleGuncelle(int? id)
        {

            IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.KitapAdi,
                    Value = k.Id.ToString()
                });
            ViewBag.KitapList =KitapList;

            if (id==null || id == 0)
            {
                return View();
            }
            else
            {
                Kiralama? KiralamaVt = _KiralamaRepository.Get(u => u.Id == id);
                if (KiralamaVt == null)
                {
                    return NotFound();
                }
                return View(KiralamaVt);
            }
            
        }
        [HttpPost]
        public IActionResult EkleGuncelle(Kiralama Kiralama)
        {
            if (ModelState.IsValid)
            {
               // var errors = ModelState.Values.SelectMany(x => x.Errors);
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string kitapPath = Path.Combine(wwwRootPath, @"img");
                /*
                if (file !=null)
                {


                    using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), (FileMode.Create)))
                    {
                        file.CopyTo(fileStream);
                    }
                    kitap.ResimUrl = @"\img\" + file.FileName;
                } */
                if(Kiralama.Id==0)
                {
                    _KiralamaRepository.Ekle(Kiralama);
                    TempData["basarili"] = "yeni Kiralama işlemi  başarıyla oluşturuldu";

                }
                else
                {
                    _KiralamaRepository.Guncelle(Kiralama);
                    TempData["basarili"] = " Kiralama güncelleme  başarıyla oluşturuldu";

                }

                //_KiralamaRepository.Ekle(kitap);
                _KiralamaRepository.Kaydet();//savechanges bilgiler veritabanına eklenmez
                TempData["basarili"] = "yeni Kiralama  başarıyla oluşturuldu";
                return RedirectToAction("Index", "Kiralama");
            }
            return View();
        }
        /*
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
        */
        /*
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
        */
        // get action 
        public IActionResult Sil(int? id)
        {
            IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.KitapAdi,
                    Value = k.Id.ToString()
                });
            ViewBag.KitapList = KitapList;
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Kiralama? KiralamaVt = _KiralamaRepository.Get(u => u.Id == id);
            if (KiralamaVt == null)
            {
                return NotFound();
            }
            return View(KiralamaVt);
        }
        [HttpPost,ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            Kiralama? Kiralama = _KiralamaRepository.Get(u => u.Id == id);
            if (Kiralama == null)
            {
                return NotFound();
            }
            _KiralamaRepository.Sil(Kiralama);
            _KiralamaRepository.Kaydet();
            TempData["basarili"] = "yeni Kiralama başarıyla silindi";
            return RedirectToAction("Index", "Kiralama");

        }


    }
}
