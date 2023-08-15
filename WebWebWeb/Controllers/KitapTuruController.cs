using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebWebWeb.Models;
using WebWebWeb.Utility;

namespace WebWebWeb.Controllers;

public class KitapTuruController : Controller
{
    private readonly IKitapTuruRepository _kitapTuruRepository;
    
    public KitapTuruController(IKitapTuruRepository context, IKitapTuruRepository kitapTuruRepository)
    {
        _kitapTuruRepository = context;
        _kitapTuruRepository = kitapTuruRepository;
    }
    
    public IActionResult Index()
    {
        List<KitapTuru> objKitapTuruList = _kitapTuruRepository.GetAll().ToList();
        IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll()
            .Select(k => new SelectListItem
            {
                Text = k.Ad,
                Value = k.Id.ToString()

            });
        return View(objKitapTuruList);
    }
    
    public IActionResult Ekle()
    {
        IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll()
            .Select(k => new SelectListItem
            {
                Text = k.Ad,
                Value = k.Id.ToString()

            });
        ViewBag.KitapTuruList = KitapTuruList;
        return View();
    }

    [HttpPost]
    public IActionResult Ekle(KitapTuru kitapTuru)
    {
        if(ModelState.IsValid)
        {
            _kitapTuruRepository.Ekle(kitapTuru);
            _kitapTuruRepository.Kaydet();
            TempData["basarili"] = "yeni kitap turu olusturuldu!";
            return RedirectToAction("Index", "KitapTuru");
        }

        return View();
    }
    public IActionResult Guncelle(int? id)   
    {
        if(id == null || id == 0)
        {
            return NotFound();
        }
        KitapTuru? kitapTuruVT = _kitapTuruRepository.Get(u => u.Id == id);
        if(kitapTuruVT== null)
        {
            return NotFound();
        }
        
        return View(kitapTuruVT);
    }
    
    [HttpPost]
    public IActionResult Guncelle(KitapTuru kitapTuru)
    {
        if(ModelState.IsValid)
        {
            _kitapTuruRepository.Guncelle(kitapTuru);
            _kitapTuruRepository.Kaydet();
            TempData["basarili"] = "kitap turu guncellendi!";
            return RedirectToAction("Index", "KitapTuru");
        }

        return View();
    }
    
    public IActionResult Sil(int? id)
    {
        if(id == null || id == 0)
        {
            return NotFound();
        }
        KitapTuru? kitapTuruVT = _kitapTuruRepository.Get(u => u.Id == id);
        if(kitapTuruVT== null)
        {
            return NotFound();
        }
        
        return View(kitapTuruVT);
    }

    [HttpPost, ActionName("Sil")]
    public IActionResult SilPOST(int? id)
    {
        KitapTuru? kitapTuru = _kitapTuruRepository.Get(u => u.Id == id);
        if (kitapTuru == null)
        {
            return NotFound();
        }
        _kitapTuruRepository.Sil(kitapTuru);
        _kitapTuruRepository.Kaydet();
        TempData["basarili"] = "kitap turu silindi!";
        return RedirectToAction("Index", "KitapTuru");
    }

}