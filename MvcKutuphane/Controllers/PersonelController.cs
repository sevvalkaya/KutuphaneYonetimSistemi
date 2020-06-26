using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using PagedList;

namespace MvcKutuphane.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index(int sayfa=1)
        {
            var degerler = db.TBLPERSONEL.OrderByDescending(x=>x.ID).ToList().ToPagedList(sayfa, 8);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(TBLPERSONEL p)
        {
            if (!ModelState.IsValid)
            {
                return View("PersonelEkle");
            }

            db.TBLPERSONEL.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");


            /*if (string.IsNullOrEmpty(p.PERSONAL))
            {
                ViewBag.mesaj = "Lütfen Personel Adı Giriniz.";

            }
            else
            {
                db.TBLPERSONEL.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();*/

        }

        public ActionResult PersonelSil(int id)
        {
            var person = db.TBLPERSONEL.Find(id);
            db.TBLPERSONEL.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelGetir(int id)
        {
            var prs = db.TBLPERSONEL.Find(id);
            return View("PersonelGetir", prs);
        }
        public ActionResult PersonelGuncelle(TBLPERSONEL p)
        {
            var prs = db.TBLPERSONEL.Find(p.ID);
            prs.PERSONAL = p.PERSONAL;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}