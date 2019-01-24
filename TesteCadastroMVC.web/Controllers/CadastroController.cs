using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteCadastroMVC.web.Models;

namespace TesteCadastroMVC.web.Controllers
{
    public class CadastroController : Controller
    {
        [Authorize]
        public ActionResult Usuarios()
        {
            return View();
        }
    }
}