using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MvcProje.Models;
using MvcProje.Utility;

namespace MvcProje.Controllers
{
     
    public class KitapController:Controller
    {
        private readonly IKitapRepository _kitapRepository;
        private readonly IKitapTuruRepository _kitapTuruRepository;
        private readonly IKitapTuruRepository kitapTuruRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public KitapController(IKitapRepository kitapRepository, IKitapTuruRepository kitapTuruRepository, IWebHostEnvironment webHostEnvironment)
        {
            _kitapRepository = kitapRepository;
            _kitapTuruRepository = kitapTuruRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        [Authorize(Roles ="Admin,Ogrenci")]//sadece bunun girmesini sağlar.
        public IActionResult Index()
        {
            // List<Kitap> objKitapList = _kitapRepository.GetAll().ToList();
            // IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll(includeProps:"KitapTuru")
            
            List<Kitap> objKitapList = _kitapRepository.GetAll(includeProps: "KitapTuru").ToList();
            return View(objKitapList);
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
        [Authorize(Roles = UserRoles.Role_Admin)]//sadece bunun girmesini sağlar.
        public IActionResult EkleGuncelle(int? id)
        {

            IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.Ad,
                    Value = k.Id.ToString()
                });
            ViewBag.KitapTuruList =KitapTuruList;
            if (id==null || id == 0)
            {
                return View();
            }
            else
            {
                Kitap? kitapVt = _kitapRepository.Get(u => u.Id == id);
                if (kitapVt == null)
                {
                    return NotFound();
                }
                return View(kitapVt);
            }
            
        }
        [HttpPost]
        [Authorize(Roles = UserRoles.Role_Admin)]//sadece bunun girmesini sağlar.
        public IActionResult EkleGuncelle(Kitap kitap, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors);
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string kitapPath = Path.Combine(wwwRootPath, @"img");

                if (file !=null)
                {


                    using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), (FileMode.Create)))
                    {
                        file.CopyTo(fileStream);
                    }
                    kitap.ResimUrl = @"\img\" + file.FileName;
                }
                if(kitap.Id==0)
                {
                    _kitapRepository.Ekle(kitap);
                    TempData["basarili"] = "yeni kitap  başarıyla oluşturuldu";

                }
                else
                {
                    _kitapRepository.Guncelle(kitap);
                    TempData["basarili"] = " kitap güncelleme  başarıyla oluşturuldu";

                }

                //_kitapRepository.Ekle(kitap);
                _kitapRepository.Kaydet();//savechanges bilgiler veritabanına eklenmez
                TempData["basarili"] = "yeni kitap  başarıyla oluşturuldu";
                return RedirectToAction("Index", "Kitap");
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
        [Authorize(Roles = UserRoles.Role_Admin)]//sadece bunun girmesini sağlar.
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
        [Authorize(Roles = UserRoles.Role_Admin)]//sadece bunun girmesini sağlar.
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
