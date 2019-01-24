using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TesteCadastroMVC.web.Models;

namespace TesteCadastroMVC.web.Controllers
{
    public class ContaController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(ViewModels user, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            var achou = (UsuarioModel.ValidarUsuario(user.Login.Usuario, user.Login.Senha));

            if (achou)
            {
                FormsAuthentication.SetAuthCookie(user.Login.Usuario, user.Login.LembrarMe);
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("","Usuário inválido !");
            }

            return View(user);
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Cadastrar(ViewModels cadastrar)
        {
            if (!ModelState.IsValid)
            {
                return View(cadastrar);
            }

            var ret = false;
            using (var conexao = new NpgsqlConnection())
            {
                conexao.ConnectionString = "Server = localhost; Port = 5432; User Id = postgres; Password = 1234; Database = Csharp";
                conexao.Open();
                using (var comando = new NpgsqlCommand())
                {
                    comando.Connection = conexao;
                    comando.CommandText = string.Format("SELECT COUNT(*) FROM usuarios WHERE login = '{0}' and senha = '{1}'", cadastrar.User.Login, cadastrar.User.Senha);
                    ret = (long)comando.ExecuteScalar() > 0;
                    if (ret)
                    {
                        ModelState.AddModelError("", "Usuário ja existe");
                        conexao.Close();
                    }
                    else
                    {
                        comando.CommandText = string.Format("INSERT INTO usuarios (login, senha) VALUES ('{0}', '{1}');", cadastrar.User.Login, cadastrar.User.Senha);
                        comando.ExecuteNonQuery();
                        conexao.Close();
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}