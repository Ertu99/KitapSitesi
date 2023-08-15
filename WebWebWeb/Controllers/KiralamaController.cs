using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebWebWeb.Models;
using WebWebWeb.Utility;

namespace WebWebWeb.Controllers;

public class KiralamaController : Controller
{
    private readonly IKiralamaRepository _kiralamaRepository;
    private readonly IKitapRepository _kitapRepository;
    public readonly IWebHostEnvironment _webHostEnvironment;
    
    public KiralamaController(IKiralamaRepository kiralamaRepository, IKitapRepository kitapRepository, IWebHostEnvironment webHostEnvironment)
    {
        _kiralamaRepository = kiralamaRepository;
        _kitapRepository = kitapRepository;
        _webHostEnvironment = webHostEnvironment;
    }
    
    public IActionResult Index()
    {
        List<Kiralama> objKiralamaList = _kiralamaRepository.GetAll(includeProps:"Kitap").ToList();
        
        return View(objKiralamaList);
    }

    public IActionResult EkleGuncelle(int? id)
    {
        IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll()
            .Select(k => new SelectListItem
            {
                Text = k.KitapAdi,
                Value = k.Id.ToString()
            });
        ViewBag.KitapList = KitapList;

        if (id == null || id==0)
        {
            // ekle
            return View();
        }
        else
        {
            // guncelleme
            Kiralama? kiralamaVt = _kiralamaRepository.Get(u => u.Id == id);  // Expression<Func<T, bool>> filtre
            if (kiralamaVt == null)
            {
                return NotFound();
            }
            return View(kiralamaVt);
        }
			
    }

    [HttpPost]
    public IActionResult EkleGuncelle(Kiralama kiralama)
    {
        if(ModelState.IsValid)
        {
            
            if (kiralama.Id == 0)
            {
                _kiralamaRepository.Ekle(kiralama);
                TempData["basarili"] = "Yeni Kiralama islemi basarili!";

            }
            else
            {
                _kiralamaRepository.Guncelle(kiralama);
                TempData["basarili"] = "Kitap kiralama kayit guncelleme Basarili!";
            }
            
            _kiralamaRepository.Kaydet();
            return RedirectToAction("Index", "Kiralama");
        }

        return View();
    }
    
    public IActionResult Sil(int? id)
    {
        IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll()
            .Select(k => new SelectListItem
            {
                Text = k.KitapAdi,
                Value = k.Id.ToString()
            });
        ViewBag.KitapList = KitapList;
        
        if(id == null || id == 0)
        {
            return NotFound();
        }
        Kiralama? kiralamaVT = _kiralamaRepository.Get(u => u.Id == id);
        if(kiralamaVT== null)
        {
            return NotFound();
        }
        
        return View(kiralamaVT);
    }

    [HttpPost, ActionName("Sil")]
    public IActionResult SilPOST(int? id)
    {
        Kiralama? kiralama = _kiralamaRepository.Get(u => u.Id == id);
        if (kiralama == null)
        {
            return NotFound();
        }
        _kiralamaRepository.Sil(kiralama);
        _kiralamaRepository.Kaydet();
        TempData["basarili"] = "kiralanan kitap silindi!";
        return RedirectToAction("Index", "Kiralama");
    }

}