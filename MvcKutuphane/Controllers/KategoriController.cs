using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using PagedList;

namespace MvcKutuphane.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();//db nesnesi DBKUTUPHANEEntities içerisinde bulunan tabloları ve propertileri tutmakta.

        public ActionResult Index(int sayfa = 1)
        {
            //var degerler = db.TBLKATEGORI.ToList();
            //degerler = degerler.OrderBy(o => o.ID).ToList();
            //db.TBLKATEGORI.OrderByDescending(o => o.ID).FirstOrDefault();
            //var degerler = db.TBLKATEGORI.OrderByDescending(o => o.ID);
            //
            var degerler = db.TBLKATEGORI.Where(a => a.DURUM == true).OrderByDescending(a => a.ID).ToList().ToPagedList(sayfa, 8);
            return View(degerler);

            /*var degerler = (from a in db.TBLKATEGORI
                            where a.DURUM == true
                            orderby a.ID descending
                            select a).ToList().ToPagedList(sayfa, 6);

            return View(degerler);*/
            
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(TBLKATEGORI p)
        {

            /*if (string.IsNullOrEmpty(p.AD))
            {
                ViewBag.mesaj = "Lütfen Kategori Adı Giriniz.";
            }
            else
            {
                db.TBLKATEGORI.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();*/
            if (!ModelState.IsValid)
            {
                return View("KategoriEkle");
            }
            
            p.DURUM = true;
            db.TBLKATEGORI.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult KategoriSil(int id)
        {
            var kategori = db.TBLKATEGORI.Find(id);
            //db.TBLKATEGORI.Remove(kategori);
            kategori.DURUM = false;//id ye göre gelen kategorinin durumunu false oluyor.
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktg = db.TBLKATEGORI.Find(id);
            ktg.DURUM = true;
            return View("KategoriGetir", ktg);
        }
        public ActionResult KategoriGuncelle(TBLKATEGORI p)
        {
            var ktg = db.TBLKATEGORI.Find(p.ID);
            if (string.IsNullOrEmpty(p.AD))
            {
                ViewBag.mesaj = "Lütfen Kategori Adı Giriniz.";
               return RedirectToAction("Index");
            }
            else
            {
                ktg.AD = p.AD;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
        }
    }


}