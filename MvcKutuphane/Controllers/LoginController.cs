using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using System.Web.Security;

namespace MvcKutuphane.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        [HttpGet]
        public ActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GirisYap(TBLUYELER t, TBLPERSONEL p)
        {
            var bilgiler = db.TBLUYELER.FirstOrDefault(x=>x.MAIL==t.MAIL && x.SIFRE==t.SIFRE);
            var bilgiler2 = db.TBLPERSONEL.FirstOrDefault(y => y.MAIL == p.MAIL && y.SIFRE == p.SIFRE);
            if (bilgiler!=null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.MAIL,false);
                Session["Mail"] = bilgiler.MAIL.ToString();
                Session["Ad"] = bilgiler.AD.ToString();
                Session["Soyad"] = bilgiler.SOYAD.ToString();
                //TempData["Id"] = bilgiler.ID.ToString();
                //TempData["Ad"] = bilgiler.AD.ToString();
                //TempData["Soyad"] = bilgiler.SOYAD.ToString();
                //TempData["KullanıcıAdı"] = bilgiler.KULLANICIADI.ToString();
                //TempData["Sifre"] = bilgiler.SIFRE.ToString();
                //TempData["Okul"] = bilgiler.OKUL.ToString();
                return RedirectToAction("Index","Panelim");
            }
            if (bilgiler2 != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler2.MAIL, false);
                Session["Mail"] = bilgiler2.MAIL.ToString();
                return RedirectToAction("Index", "istatistik");
            }
            else
            {
                return View();
            }
           
        }
        
    }
}