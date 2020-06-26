using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using PagedList;

namespace MvcKutuphane.Controllers
{
    public class YazarController : Controller
    {
        // GET: Yazar
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

        public ActionResult Index(int sayfa = 1)
        {
            var degerler = db.TBLYAZAR.OrderByDescending(o => o.ID).ToList().ToPagedList(sayfa, 8); ;
            return View(degerler);
            /*var degerler = db.TBLYAZAR.ToList();
            return View(degerler);*/
        }
        [HttpGet]
        public ActionResult YazarEkle()
        {

            return View();
        }
        [HttpPost]
        public ActionResult YazarEkle(TBLYAZAR p)
        {
            if (!ModelState.IsValid)
            {
                return View("YazarEkle");
            }
            //p den gelen değerleri YazarEkle view'e göndercez
            db.TBLYAZAR.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult YazarSil(int id)
        {
            var yazar = db.TBLYAZAR.Find(id);
            db.TBLYAZAR.Remove(yazar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult YazarGetir(int id)
        {
            var yzr = db.TBLYAZAR.Find(id);
            return View("YazarGetir", yzr);
            //YazarGetir ActionResultı içerisindeki yzr değerime göre o sayfadaki değeri getirecek.
        }
        public ActionResult YazarKitaplar(int id)
        {
            var yazar = db.TBLKITAP.Where(x=>x.YAZAR==id).ToList();
            var yzrad = db.TBLYAZAR.Where(y => y.ID == id).Select(z => z.AD + " " + z.SOYAD).FirstOrDefault();
            ViewBag.yazar1 = yzrad;
            return View(yazar);
        }

    }
}