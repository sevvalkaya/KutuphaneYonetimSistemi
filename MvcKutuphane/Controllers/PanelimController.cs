using MvcKutuphane.Models.Entity;
using System.Linq;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using System.Web.Security;

namespace MvcKutuphane.Controllers
{
    public class PanelimController : Controller
    {
        // GET: Panelim
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        [HttpGet]
        [Authorize]// indez authorize bağlı olarak çalışcak
        public ActionResult Index()
        {
            var uyemail=(string)Session["Mail"];
            
            var degerler=db.TBLUYELER.FirstOrDefault(x => x.MAIL == uyemail);
            return View(degerler);
        }
        [HttpPost]
        public ActionResult Index(TBLUYELER p)
        {
            var kullanici = (string)Session["Mail"];//session içerisinndeki mail adlı kullanıcı bilgisine kullanici değişkenine atadım 
            var uye = db.TBLUYELER.FirstOrDefault(x => x.MAIL == kullanici);
            uye.SIFRE = p.SIFRE;
            uye.AD = p.AD;
            uye.FOTOGRAF = p.FOTOGRAF;
            uye.OKUL = p.OKUL;
            uye.KULLANICIADI = p.KULLANICIADI;

            db.SaveChanges();
            
            return View();
        }
        public ActionResult Kitaplarım()
        {
            var kullanici = (string)Session["Mail"];
            var id = db.TBLUYELER.Where(x=>x.MAIL==kullanici.ToString()).Select(z=>z.ID).FirstOrDefault();//Kullanıcdan gelen maili listeleme
            var degerler = db.TBLHAREKET.Where(x => x.UYE == id).ToList();//işlemdurumu id ye eşit olanların listesini bana getir
            return View(degerler);
            
        }
        public ActionResult Duyurular()
        {
            var duyurulistesi = db.TBLDUYURULAR.ToList();
            return View(duyurulistesi);

        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap","Login");

        }

    }
}