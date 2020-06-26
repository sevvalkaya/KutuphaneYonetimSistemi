using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class istatistikController : Controller
    {
        // GET: istatistik
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

        public ActionResult Index()
        {

            //var kullanici = (string)Session["Mail"];//session içerisinndeki mail adlı kullanıcı bilgisine kullanici değişkenine atadım 
            //var personel = db.TBLPERSONEL.FirstOrDefault(x => x.MAIL == kullanici);
            
            var deger1 = db.TBLUYELER.Count();
            ViewBag.dgr1 = deger1;

            var deger2 = db.TBLKITAP.Count();//kaç tane kitap var cardlara yazdırıyoruz istatistik indexinde
            ViewBag.dgr2 = deger2;

            var deger3 = db.TBLKITAP.Where(x=>x.DURUM==false).Count();
            ViewBag.dgr3 = deger3;

            var deger4 = db.TBLCEZALAR.Sum(x => x.PARA);
            ViewBag.dgr4 = deger4;
            return View();
        }
        public ActionResult HavaKart()
        {
            return View();
        }
        public ActionResult Hava()
        {
            return View();
        }
        public ActionResult Galeri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult resimyukle(HttpPostedFileBase dosya)//dosya ile input submitteki name aynı olmak zorunda
        {
            if (dosya.ContentLength > 0)//dosyanın içeriğinin boyutu 0 dan büyükse
            {
                string dosyayolu = Path.Combine(Server.MapPath("~/web2/resimler/"),
                Path.GetFileName(dosya.FileName));

                dosya.SaveAs(dosyayolu);


            }
            return RedirectToAction("Galeri");
        }
        public ActionResult LinqKart()
        {
            return View();
        }
    }

}