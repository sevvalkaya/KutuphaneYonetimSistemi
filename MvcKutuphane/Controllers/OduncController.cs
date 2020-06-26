using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web.Mvc;

namespace MvcKutuphane.Controllers
{
    public class OduncController : Controller
    {
        // GET: Odunc
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var degerler = db.TBLHAREKET.Where(x => x.ISLEMDURUM == false).ToList();//işlemdurumu false olanların listesini bana getir
            return View(degerler);
        }
        [HttpGet]
        public ActionResult OduncVer()
        {
            List<SelectListItem> deger = (from x in db.TBLUYELER.ToList()
                                          select new SelectListItem
                                          {
                                              Text = x.AD + " " + x.SOYAD,
                                              Value = x.ID.ToString()
                                          }).ToList();
            ViewBag.dgr1 = deger;

            List<SelectListItem> deger2 = (from y in db.TBLKITAP.Where(y=>y.DURUM==true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = y.AD,
                                               Value = y.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            List<SelectListItem> deger3 = (from z in db.TBLPERSONEL.ToList()
                                           select new SelectListItem
                                           {
                                               Text = z.PERSONAL,
                                               Value = z.ID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            return View();
        }
        [HttpPost]
        public ActionResult OduncVer(TBLHAREKET p)
        {
            //DEĞİŞKENLERİMİ DROPDOWNLİSTTEN GELEN VERİLERİLERİ EŞLEŞTİRDİM
            var d1 = db.TBLUYELER.Where(x => x.ID == p.TBLUYELER.ID).FirstOrDefault();
            var d2 = db.TBLKITAP.Where(y => y.ID == p.TBLKITAP.ID).FirstOrDefault();
            var d3 = db.TBLPERSONEL.Where(z => z.ID == p.PERSONEL).FirstOrDefault();
            p.TBLUYELER = d1;
            p.TBLKITAP = d2;
            //p.PERSONEL = d3;

            db.TBLHAREKET.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult OduncIade(TBLHAREKET p)
        {
            var odn = db.TBLHAREKET.Find(p.ID);
            DateTime d1 = DateTime.Parse(odn.IADETARIH.ToString());//başlangıçtaki kitabın verilmesi gereken tarih
            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());//bugünün tarihini alıyoruz
            TimeSpan d3 = d2 - d1;// timespan iki gün arasındaki farkı bulmamızı sağlıyor.
            ViewBag.dgr = d3.TotalDays;//totaldays toplam gün sayısını veriyor.
            return View("OduncIade", odn);
        }
        public ActionResult OduncGuncelle(TBLHAREKET p)
        {
            var hrk = db.TBLHAREKET.Find(p.ID);
            hrk.UYEGETIRTARIH = p.UYEGETIRTARIH;
            hrk.ISLEMDURUM = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}