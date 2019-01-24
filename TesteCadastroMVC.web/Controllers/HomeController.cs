using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TesteCadastroMVC.web.Controllers
{
    public class HomeController : Controller
    {
        [Route("Home")]
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
    }
}