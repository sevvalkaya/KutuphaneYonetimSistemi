using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using System.Web.Security;
using PagedList;

namespace MvcKutuphane.Controllers
{
    public class islemController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        // GET: islem
        public ActionResult Index(int sayfa=1)
        {
            var degerler = db.TBLHAREKET.Where(x => x.ISLEMDURUM == true).ToList().ToPagedList(sayfa, 8);//işlemdurumu false olanların listesini bana getir
            return View(degerler);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap", "Login");

        }
    }
}